using SomeApp.Contracts.Dto.TaskRows;

namespace SomeApp.UseCases.Commands.TaskRows.DeleteTaskRow
{
    /// <summary>
    /// Структура данных ответа по удалению задачи
    /// </summary>
    public class DeleteTaskRowResponse
    {
        /// <summary>
        /// Индикатор успеха
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Модель задачи
        /// </summary>
        public DeleteTaskRowResponseDto TaskRow { get; set; }
       
    }
}
