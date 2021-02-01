using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Pages.ViewModel
{
    public class IdBookAccountViewModel
    {
        public int IdAccount { get; set; }
        public int IdBook { get; set; }
        public IdBookAccountViewModel()
        {

        }
        public IdBookAccountViewModel(int idBook,int idAccount)
        {
            IdBook = idBook;
            IdAccount = idAccount;
        }
    }
}
