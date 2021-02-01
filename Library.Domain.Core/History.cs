using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace Library.Domain.Core
{
    public class History
    {
        public History(int idAccount, int idBook)
        {
            StartDate = DateTime.Now;
            IdAccount = idAccount;
            IdBook = idBook;
        }
        public int Id { get; set; }

        [MaybeNull]
        [Column(TypeName = "datetime2")]
        /// <summary>
        /// Дата взятие
        /// </summary>
        public DateTime StartDate { get; set; }

        [MaybeNull]
        [Column(TypeName = "datetime2")]
        /// <summary>
        /// Дата возврата
        /// </summary>
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
        /// Время, в течение которого книга находилась у пользователя
        /// </summary>
        [NotMapped]
        public int BookingTime { get 
            {
                if (EndDate != null)
                    return (EndDate - StartDate).Days;
                return 0;
            } 
        }

        /// <summary>
        /// Время бронирования, которое задал библиотекарь
        /// </summary>
        public int BookingTimeLib { get; set; }

        public int IdAccount { get; set; }
        public int IdBook { get; set; }
    }
}
