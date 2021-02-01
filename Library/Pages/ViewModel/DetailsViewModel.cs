using Library.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Pages.ViewModel
{
    public class DetailsViewModel
    {
        public Book DetailBook { get; set; }
        public int DetailIdAccount { get; set; }
        public DetailsViewModel(Book book, int idAccount)
        {
            DetailBook = book;
            DetailIdAccount = idAccount;
        }
    }
}
