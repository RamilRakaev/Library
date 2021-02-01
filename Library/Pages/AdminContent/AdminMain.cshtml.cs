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
    public class AdminMainModel : PageModel
    {
        public List<Account> Accounts { get; set; }
        readonly private IAdminRepos repos;
        public AdminMainModel(IAdminRepos adminRepos)
        {
            repos = adminRepos;
        }

        public void OnGet()
        {
            Accounts = repos.Users.ToList();
        }

        public void OnGetSuperUsers()
        {
            Accounts = repos.SuperUsers.ToList();
        }

        public void OnGetDelete(int idAccount)
        {
            var account = repos.GetAccount(idAccount).Rights;
            repos.RemoveAccount(idAccount);
            if (account == "user")
                Accounts = repos.Users.ToList();
            else
                Accounts = repos.SuperUsers.ToList();
        }
    }
}
