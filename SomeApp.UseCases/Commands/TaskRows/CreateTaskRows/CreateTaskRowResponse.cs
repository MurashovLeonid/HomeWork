using SomeApp.Contracts.Dto.TaskRows;

namespace SomeApp.UseCases.Commands.TaskRows.CreateTaskRow
{
    /// <summary>
    /// Структура данных ответа по созданию задачи
    /// </summary>
    public class CreateTaskRowResponse
    {
        /// <summary>
        /// Индикатор успеха
        /// </summary>
        public bool IsSuccess { get; set; }       

        /// <summary>
        /// Модель ответа
        /// </summary>
        public CreateTaskRowResponeDto TaskRow { get; set; }
    }
}
