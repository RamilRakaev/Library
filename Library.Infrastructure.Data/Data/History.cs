using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Domain.Interfaces.IData;
using System.Diagnostics.CodeAnalysis;

namespace Library.Infrastructure.Bll.Data
{
    public class History:IHistory
    {
        public History()
        {

        }

        public History(int idAccount, int idBook,int days)
        {
            StartDate = DateTime.Now;
            IdAccount = idAccount;
            IdBook = idBook;
            BookingTimeLib = days;
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

        public void Accept(string state = "", bool loss = false)
        {
            if (state != "")
                Damage = true;
            Loss = loss;
        }
    }
}
