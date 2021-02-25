using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Library.Domain.Interfaces.IData
{
    public interface IHistory
    {
        public int Id { get; set; }


        /// <summary>
        /// Дата взятие
        /// </summary>
        [MaybeNull]
        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата возврата
        /// </summary>
        [MaybeNull]
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Факт порчи книги
        /// </summary>
        public bool Damage { get; set; }

        /// <summary>
        /// Потеря
        /// </summary>
        public bool Loss { get; set; }

        /// <summary>
        /// Время в днях, в течение которого книга находилась у пользователя
        /// </summary>
        [NotMapped]
        public int BookingTime { get; }

        /// <summary>
        /// Время бронирования, которое задал библиотекарь
        /// </summary>
        public int BookingTimeLib { get; set; }

        public int IdAccount { get; set; }
        public int IdBook { get; set; }

        /// <summary>
        /// Принять книгу
        /// </summary>
        /// <param name="idBook"></param>
        /// <param name="idAccount"></param>
        /// <param name="state"></param>
        /// <param name="loss"></param>
        public void Accept(string state = "", bool loss = false);
    }
}
