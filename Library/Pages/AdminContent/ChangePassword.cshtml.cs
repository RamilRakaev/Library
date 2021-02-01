using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.Core;
using Library.Domain.Interfaces;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.AdminContent
{
    public class ChangePasswordModel : PageModel
    {
        public Account ChangeAccount { get; set; } = new Account();
        [BindProperty]
        public bool notify { get; set; }
        readonly private IAdminRepos repos;
        readonly private INovelties novel;
        public ChangePasswordModel(IAdminRepos adminRepos,INovelties novelties)
        {
            repos = adminRepos;
            novel = novelties;
        }

        public void OnGet(int idAccount)
        {
            ChangeAccount = repos.GetAccount(idAccount);
        }

        public IActionResult OnPost(int idAccount, string newPassword)
        {
            repos.ChangePassword(idAccount, newPassword);
            if (notify)
            {
                try
                {
                    novel.SendMessage(newPassword, repos.GetAccount(idAccount));
                }
                catch
                {
                    TempData["error"] = "Произошла ошибка при отправке email";
                }
            }
            return RedirectToPage("AdminMain");
        }
    }
}
