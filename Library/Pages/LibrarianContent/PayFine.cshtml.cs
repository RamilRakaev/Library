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
    public class PayFineModel : PageModel
    {
        public Account Account { get; set; } = new Account() { Id = 0, Name = "" };
        

        readonly private ILibrarianRepos repos;
        public PayFineModel(ILibrarianRepos librarianRepos)
        {
            repos = librarianRepos;
        }

        public void OnGet(int idAccount)
        {
            Account = repos.GetAccount(idAccount);
        }

        public IActionResult OnPost(int idAccount, ulong pay)
        {
            repos.PayOffFine(idAccount, pay);
            return RedirectToPage("LibrarianMain");
        }
    }
}
