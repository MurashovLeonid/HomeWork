namespace SomeApp.Contracts.Dto.TaskRows
{
    /// <summary>
    /// Структура данных модели ответа для создания задачи
    /// </summary>
    public class CreateTaskRowResponeDto
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор связанной записи
        /// </summary>
        public int? SourceId { get; set; }

        /// <summary>
        /// Дата создания задачи
        /// </summary>
        public DateTime Added { get; set; }

        /// <summary>
        /// Информация об ошибке
        /// </summary>
        public string? FailureInfo { get; set; }
    }
}
