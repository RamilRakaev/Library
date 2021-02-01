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
    public class AcceptModel : PageModel
    {
        public Account Account { get; set; }
        public Book Book { get; set; }
        public string State { get; set; } = "";
        public ulong Penalty { get; set; } = 0;
        public bool Loss { get; set; } = false;
        public int Delay{get;set;}

        readonly private ILibrarianRepos repos;
        public AcceptModel(ILibrarianRepos librarianRepos)
        {
            repos = librarianRepos;
        }

        public void OnGet(int idBook, int idAccount)
        {
            repos.InitialAcceptBook(idBook, idAccount);
            Account = repos.GetAccount(idAccount);
            Book=repos.GetBook(idBook);
            Delay=repos.DeadlineOverdue(idAccount, idBook);
        }

        public IActionResult OnPost(int idBook, int idAccount, ulong penalty=0 , string state = "", bool loss=false)
        {
            repos.AcceptBook(idBook, idAccount, state,loss);
            if(penalty!=0)
            repos.ChargeFine(idAccount, penalty);
            return RedirectToPage("LibrarianMain");
        }
    }
}
