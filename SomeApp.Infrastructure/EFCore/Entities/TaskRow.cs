using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeApp.Infrastructure.Implementation.EFCore.Entities
{
    /// <summary>
    /// Структура данных записи о задаче
    /// </summary>
    public class TaskRow
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор записи, для которой завели задачу
        /// </summary>
        public int IdSource { get; set; }
     
        /// <summary>
        /// Информация
        /// </summary>
        public string? Information { get; set; }

        /// <summary>
        /// Дата создания задачи
        /// </summary>
        public DateTime Added { get; set; }

        /// <summary>
        /// Дата исполнения задачи
        /// </summary>
        public DateTime? Completed { get; set; }

        /// <summary>
        /// Тип задачи
        /// </summary>
        public TaskRowType TypeWork { get; set; }
      
    }
}
