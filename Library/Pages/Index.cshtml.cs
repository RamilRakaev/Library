using Library.Domain.Core;
using Library.Domain.Interfaces;
using Library.Infrastructure.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Pages
{
    public class IndexModel : PageModel
    {
        
        private readonly IAdminRepos repos;
        public List<Book> UserBooks { get; set; }
        public List<Account> Accounts { get; set; }
        public int IdAccount { get; set; }
        [BindProperty]
        public string Message { get; set; } = "Ведите имя и пароль";
        public IndexModel(IAdminRepos adminRepos)
        {
            repos = adminRepos;
            //novelties.SendMessageAboutBooks(repos.GetAccount(1),userRepos.AllBooks.ToArray());
        }
        public void OnGet()
        {
            if(TempData.ContainsKey("message"))
            {
                Message =(string)TempData["message"];
            }
            
        }

        public IActionResult OnPost(string name, string password)
        {
            var account= repos.FindByPassword(password, name);
            switch (account.Rights)
            {
                case "user":
                    TempData["id"] = account.Id;
                    return RedirectToPage("UserContent/UserMain");
                case "admin":
                    return RedirectToPage("/AdminContent/AdminMain");
                case "librarian":
                    return RedirectToPage("/LibrarianContent/LibrarianMain");
                default:
                    TempData["message"] = "Пароль или/и имя введены неверно";
                    return RedirectToPage("Index","Пароль и/или имя введены неверно");
            }
        }

        //public static void Set<T>(this ITempDataDictionary tempData,string key, T value)where T : class
        //{
        //    tempData[key] = JsonConvert.SerializeObject(value);
        //}
        public IActionResult OnGetRedirectTo()
        {
            //return RedirectToPage("./UserMain",ThisAccount);
            return RedirectToPage("/AdminContent/AdminMain");
        }
      

    }
}
