using SomeApp.Contracts.Dto.TaskRows;

namespace SomeApp.Infrastructure.Interfaces.RepositoryInterfaces
{
    /// <summary>
    /// Интерфейс для работы с репозиторием
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Создание задачи
        /// </summary>
        /// <param name="createTaskRowDto"></param>
        /// <returns></returns>
        Task<CreateTaskRowResponeDto> CreateTaskRowAsync(CreateTaskRowDto createTaskRowDto);

        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DeleteTaskRowResponseDto> DeleteTaskRowAsync(int id);

        /// <summary>
        /// Получить задачу по идентификатору, или по идентификатору связанной записи
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        Task<GetTaskRowByIdDto> GetTaskRowByIdAsync(int id);

        /// <summary>
        /// Добавляет записи
        /// </summary>
        /// <param name="taskRowIds"></param>
        /// <returns></returns>
        Task CreateEntryRowsAsync();

        /// <summary>
        /// Удаляет записи
        /// </summary>
        /// <param name="taskRowIds"></param>
        /// <returns></returns>
        Task DeleteEntryRowsAsync();

        /// <summary>
        /// Обновляет записи
        /// </summary>
        /// <param name="taskRowIds"></param>
        /// <returns></returns>
        Task UpdateEntryRowsAsync();
    }
}
