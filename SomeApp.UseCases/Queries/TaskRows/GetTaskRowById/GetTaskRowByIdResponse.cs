using SomeApp.Contracts.Dto.TaskRows;

namespace SomeApp.UseCases.Queries.TaskRows.GetTaskRowById
{
    /// <summary>
    /// Струтура данных для ответа по запросу вывода задачи по Id
    /// </summary>
    public class GetTaskRowByIdResponse
    {
        /// <summary>
        /// Модель задачи
        /// </summary>
        public GetTaskRowByIdDto? TaskRowDto { get; set; }

        /// <summary>
        /// Индикатор успеха операции
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Описание проблемы
        /// </summary>
        public string? FailureInfo { get; set; }
    }
}
