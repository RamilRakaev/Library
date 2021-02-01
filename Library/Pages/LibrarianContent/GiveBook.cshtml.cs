using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.Core;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.LibrarianContent
{
    public class GiveBookModel : PageModel
    {
        public Account Account { get; set; }
        public Book Book { get; set; }
        public int DaysBookint { get; set; }

        readonly private ILibrarianRepos repos;
        public GiveBookModel(ILibrarianRepos librarianRepos)
        {
            repos = librarianRepos;
        }

        public void OnGet(int idBook, int idAccount)
        {
            Account = repos.GetAccount(idAccount);
            Book = repos.GetBook(idBook);
        }


        public IActionResult OnPost(int idBook, int idAccount, int days = 0)
        {
            repos.GiveBook(idBook, idAccount, days);
            return RedirectToPage("LibrarianMain");
        }
    }
}
