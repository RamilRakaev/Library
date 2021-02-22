using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Domain.Interfaces.IData
{
    public interface IComment
    {
        public int Id { get; set; }

        public int IdAccount { get; set; }
        public int IdBook { get; set; }

        /// <summary>
        /// Имя комментатора
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string TextComment { get; set; }

        [Column(TypeName = "datetime2")]
        /// <summary>
        /// Дата комментария
        /// </summary>
        public DateTime Date { get; set; }
    }
}
