namespace SomeApp.Contracts.Dto.TaskRows
{
    /// <summary>
    /// Структура данных для создания задачи
    /// </summary>
    public class CreateTaskRowDto
    {
        /// <summary>
        /// Информация
        /// </summary>
        public string? Information { get; set; }

        /// <summary>
        /// Идентификатор строки, для которой заводится задача
        /// </summary>
        public int? IdSource { get; set; }

        /// <summary>
        /// Тип задачи
        /// </summary>
        public TaskRowTypeDto TaskRowType { get; set; }

    }
}
