using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.Core;
using Library.Domain.Interfaces;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.LibrarianContent
{
    public class LibrarianMainModel : PageModel
    {
        public List<Book> Books { get; set; }
        public List<Account> Accounts { get; set; }

        public bool ShowBooks { get; set; } = true;

        public Book ChosenBook { get; set; }
        public Account ChosenAccount { 
            get 
            {
                if (ChosenBook == null | !ChosenBook.IsBusy | ChosenBook.IdAccount==0)
                {
                    return new Account() { Id = 0, Name = "Пустой аккаунт"};
                }

                return repos.GetAccount(ChosenBook.IdAccount);
                
            } 
        }

        readonly private ILibrarianRepos repos;
        public LibrarianMainModel(ILibrarianRepos librarianRepos)
        {
            repos = librarianRepos;
            Accounts = repos.Users.ToList();
        }

        public void OnGet()
        {
            Books = repos.AllBooks.ToList();
            Accounts = repos.Users.ToList();
        }

        public void OnGetAllBusyBooks()
        {
            Books = repos.AllBusyBooks.ToList();
            Accounts = repos.Users.ToList();
        }

        public void OnGetGivenBooks()
        {
            Books = repos.GivenBooks.ToList();
            Accounts = repos.Users.ToList();
        }

        public void OnGetFreeBooks()
        {
            Books = repos.FreeBooks.ToList();
            Accounts = repos.Users.ToList();
        }

        public void OnGetShowAccounts()
        {
            ShowBooks = false;
        }
    }
}
