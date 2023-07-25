namespace SomeApp.Contracts.Dto.TaskRows
{
    /// <summary>
    /// Структура данных для ответа на клиент
    /// </summary>
    public class GetTaskRowByIdDto
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор связанной записи
        /// </summary>
        public int IdSource { get; set; }

        /// <summary>
        /// Дата создания задачи
        /// </summary>
        public DateTime Added { get; set; }

        /// <summary>
        /// Информация
        /// </summary>
        public string? Information { get; set; }

        /// <summary>
        /// Тип задачи
        /// </summary>
        public TaskRowTypeDto TypeWork { get; set; }

        /// <summary>
        /// Дата завершения задачи
        /// </summary>
        public DateTime? Completed { get; set; }

    }
}
