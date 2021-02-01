using Library.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Pages.ViewModel
{
    public class BookAccountObjViewModel
    {
        public Book BookObj { get; set; }
        public Account AccountObj { get; set; }
        public BookAccountObjViewModel(Book book, Account account)
        {
            BookObj = book;
            AccountObj = account;
        }
    }
}
