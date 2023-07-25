using Microsoft.EntityFrameworkCore;
using SomeApp.Contracts.Dto.TaskRows;
using SomeApp.Infrastructure.Implementation.EFCore.Contexts;
using SomeApp.Infrastructure.Implementation.EFCore.Entities;
using SomeApp.Infrastructure.Interfaces.RepositoryInterfaces;
using System.Security.Cryptography.X509Certificates;

namespace SomeApp.Infrastructure.Implementation.EFCore.RepositoryImplementations
{
    /// <summary>
    /// Структура данных для работы с провайдером БД
    /// </summary>
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Логика создания, удаления, получения задач

        /// <summary>
        /// Создает задачу
        /// </summary>
        /// <param name="createTaskRowDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CreateTaskRowResponeDto> CreateTaskRowAsync(CreateTaskRowDto createTaskRowDto)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                // Если идентификатора связанной записи нет, то по логике заводится задача на создание записи
                if (createTaskRowDto.IdSource == null)
                {
                  
                    var taskRow = new TaskRow()
                    {                     
                        Information = createTaskRowDto.Information,
                        Added = DateTime.UtcNow,
                        TypeWork = CastTypeOfWorkDtoToDal(createTaskRowDto.TaskRowType),
                    };

                    _context.Add(taskRow);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new CreateTaskRowResponeDto() { Id = taskRow.Id, Added = taskRow.Added };
                }
                // Если идентификатор есть, то заводится задача на изменение/удаление записи
                else
                {
                    var baseTaskRow = await _context.TaskRows.AsNoTracking().Where(x => x.Completed == null && x.IdSource == createTaskRowDto.IdSource).FirstOrDefaultAsync();
                    
                    //Если есть незакрытые задачи, новые создавать нельзя
                    if (baseTaskRow != null)
                    {
                        return new CreateTaskRowResponeDto 
                        { 
                            Id = baseTaskRow.Id, 
                            Added = baseTaskRow.Added,
                            SourceId = baseTaskRow.IdSource,
                            FailureInfo = $"Для записи с идентификатором {createTaskRowDto.IdSource} есть незакрытые задачи" 
                        };
                    }

                    // на случай, если захотели поставить задачу на изменение/удаление записи, а она уже удалена
                    var entryRow = await _context.EntryRows.AsNoTracking().Where(x => x.Id == createTaskRowDto.IdSource).SingleOrDefaultAsync();

                    // Проверяем что у нужной записи отметка удаления уже заполнена
                    if (entryRow.Deleted != null)
                    {
                        return new CreateTaskRowResponeDto
                        {                        
                            FailureInfo = $"Запись с идентификатором {createTaskRowDto.IdSource} уже удалена"
                        };
                    }

                    var taskRow = new TaskRow()
                    {
                        IdSource = (int)createTaskRowDto.IdSource,
                        Information = createTaskRowDto.Information,
                        Added = DateTime.UtcNow,
                        TypeWork = CastTypeOfWorkDtoToDal(createTaskRowDto.TaskRowType),
                    };

                    _context.Add(taskRow);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new CreateTaskRowResponeDto()
                    {
                        Id = taskRow.Id,
                        Added = taskRow.Added,
                        SourceId = taskRow.TypeWork == TaskRowType.Delete ? taskRow.IdSource : null
                    };
                }

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                transaction.Dispose();
            }

        }

        /// <summary>
        /// Удаляет задачу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DeleteTaskRowResponseDto> DeleteTaskRowAsync(int id)
        {

            var taskRow = await _context.TaskRows.Where(x => x.Id == id).SingleOrDefaultAsync();
            if (taskRow == null)
            {
                return new DeleteTaskRowResponseDto() { FailureInfo = $"Задача с идентификатором {id} не найдена" };               
            }
            if (taskRow.Completed != null)
            {
                return new DeleteTaskRowResponseDto() 
                { 
                    Id =taskRow.Id,
                    Added = taskRow.Added,
                    Completed = taskRow.Completed,
                    FailureInfo = $"Задача с идентификатором {id} уже завершена",  
                };
            }           
            _context.Remove(taskRow);

            await _context.SaveChangesAsync();

            return new DeleteTaskRowResponseDto()
            {
                Id = taskRow.Id,
                Added = taskRow.Added
            };
        }

        /// <summary>
        /// Получает задачу по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<GetTaskRowByIdDto> GetTaskRowByIdAsync(int id)
        {
            var baseTask = await _context.TaskRows.AsNoTracking()
                .Where(x =>  x.Id == id)
                .SingleOrDefaultAsync();

            if (baseTask == null)
            {
                throw new Exception($"Задача с идентификатором {id} не найдена");
            }

            return new GetTaskRowByIdDto()
            {

                Id = baseTask.Id,
                IdSource = baseTask.IdSource,
                Added = baseTask.Added,
                Completed = baseTask.Completed,
                Information = baseTask.Information,
                TypeWork = CastDalToTypeOfWorkDto(baseTask.TypeWork),
            };
        }

        #endregion


        #region Операции работы с записями через поставленные задачи

        /// <summary>
        /// Добавляет записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task CreateEntryRowsAsync()
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                
                var taskRows = await _context.TaskRows.Where(x => x.Completed == null && x.TypeWork == TaskRowType.Create).ToListAsync();
                    
                if (taskRows.Any())
                {
                    foreach (var taskRow in taskRows)
                    {
                        await AddEntryToDb(taskRow);
                    }                               

                    await transaction.CommitAsync();
                }
                
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
        }

        /// <summary>
        /// Удаляет записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteEntryRowsAsync()
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                var taskRows = await _context.TaskRows.Where(x => x.Completed == null && x.TypeWork == TaskRowType.Delete)
                    .Join(
                    _context.EntryRows,
                    tr => tr.IdSource,
                    er => er.Id,
                    (tr, er) => new KeyValuePair<TaskRow, EntryRow>(tr, er)
                    ).ToDictionaryAsync(x => x.Key, x => x.Value);

                if (taskRows.Any())
                {
                    Parallel.ForEach(taskRows, x => SetDeleted(x));

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }

                
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
        }

        /// <summary>
        /// Обновляет записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task UpdateEntryRowsAsync()
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                var taskRows = await _context.TaskRows.Where(x => x.Completed == null && x.TypeWork == TaskRowType.Update)
                    .Join(
                    _context.EntryRows,
                    tr => tr.IdSource,
                    er => er.Id,
                    (tr, er) => new KeyValuePair<TaskRow, EntryRow>(tr, er)
                    ).ToDictionaryAsync(x => x.Key, x => x.Value);

                if (taskRows.Any())
                {
                    Parallel.ForEach(taskRows, x => SetUpdated(x));

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }

               
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
        }

        #endregion


        #region Приватные методы
        /// <summary>
        /// Кастит енам из дто к енаму из data access layer
        /// </summary>
        /// <param name="typeWorkDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private TaskRowType CastTypeOfWorkDtoToDal(TaskRowTypeDto typeWorkDto)
        {
            switch (typeWorkDto)
            {
                case TaskRowTypeDto.Update: return TaskRowType.Update;
                case TaskRowTypeDto.Delete: return TaskRowType.Delete;
                case TaskRowTypeDto.Create: return TaskRowType.Create;
                default: throw new NotImplementedException("Отсутствует необходимый тип задачи");
            }
        }

        /// <summary>
        /// Кастит енам из data access layer к енаму из дто
        /// </summary>
        /// <param name="typeWorkDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private TaskRowTypeDto CastDalToTypeOfWorkDto(TaskRowType typeWorkDto)
        {
            switch (typeWorkDto)
            {
                case TaskRowType.Update: return TaskRowTypeDto.Update;
                case TaskRowType.Delete: return TaskRowTypeDto.Delete;
                case TaskRowType.Create: return TaskRowTypeDto.Create;
                default: throw new NotImplementedException("Отсутствует необходимый тип задачи");
            }
        }


        /// <summary>
        /// Добавляет записи в контекст и завершает операции добавления записей
        /// </summary>
        /// <param name="taskRow"></param>
        private async Task AddEntryToDb(TaskRow taskRow)
        {
            var row = new EntryRow()
            {
                Added = DateTime.UtcNow,
                Information = taskRow.Information
            };
            _context.EntryRows.Add(row);

            await _context.SaveChangesAsync();

            taskRow.Completed = DateTime.UtcNow;

            taskRow.IdSource = row.Id;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет записи и завершает операции удаления записей
        /// </summary>
        /// <param name="pair"></param>
        private void SetDeleted(KeyValuePair<TaskRow, EntryRow> pair)
        {
            pair.Key.Completed = DateTime.UtcNow;
            pair.Value.Deleted = DateTime.UtcNow;
        }

        /// <summary>
        /// Изменяет записи и завершает операции изменения записей
        /// </summary>
        /// <param name="pair"></param>
        private void SetUpdated(KeyValuePair<TaskRow, EntryRow> pair)
        {
            pair.Key.Completed = DateTime.UtcNow;
            pair.Value.Information = pair.Key.Information;
        }
        #endregion
    }
}
