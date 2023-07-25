using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeApp.Contracts.Dto.TaskRows
{
    /// <summary>
    /// Структура данных для ответа на запрос удаления задачи
    /// </summary>
    public class DeleteTaskRowResponseDto
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Время создания задачи
        /// </summary>
        public DateTime Added { get; set; }

        /// <summary>
        /// Время исполнения задачи
        /// </summary>
        public DateTime? Completed { get; set; }

        /// <summary>
        /// Информация об ошибке
        /// </summary>
        public string? FailureInfo { get; set; }
    }
}
