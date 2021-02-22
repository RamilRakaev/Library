using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Domain.Interfaces.IData;
using System.Diagnostics.CodeAnalysis;

namespace Library.Domain.Core
{
    public class History:IHistory
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
        public DateTime StartDate { get; set; }

        [MaybeNull]
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        public bool Damage { get; set; }

        public bool Loss { get; set; }

        [NotMapped]
        public int BookingTime { get 
            {
                if (EndDate != null)
                    return (EndDate - StartDate).Days;
                return 0;
            } 
        }

        public int BookingTimeLib { get; set; }

        public int IdAccount { get; set; }
        public int IdBook { get; set; }
    }
}
