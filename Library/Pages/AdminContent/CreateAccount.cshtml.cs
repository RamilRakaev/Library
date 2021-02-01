using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.Core;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.AdminContent
{
    public class CreateAccountModel : PageModel
    {
        public Account NewAccount { get; set; } = new Account();
        readonly private IAdminRepos repos;
        public CreateAccountModel(IAdminRepos adminRepos)
        {
            repos=adminRepos;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(Account account,string rights)
        {
            account.Rights = rights;
            repos.AddAccount(account);
            return RedirectToPage("AdminMain");
        }
    }
}
