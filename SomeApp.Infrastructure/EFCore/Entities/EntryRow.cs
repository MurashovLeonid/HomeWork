using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeApp.Infrastructure.Implementation.EFCore.Entities
{
    /// <summary>
    /// Структура данных для записи
    /// </summary>
    public class EntryRow
    {
        [Key]
        /// <summary>
        /// Структура данных для записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Время добавления записи
        /// </summary>
        public DateTime Added { get; set; }

        /// <summary>
        /// Время удаления записи
        /// </summary>
        public DateTime? Deleted { get; set; }

        /// <summary>
        /// Информация в записи
        /// </summary>
        public string? Information { get; set; }
      
    }
}
