using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.Core;
using Library.Domain.Interfaces;
using Library.Pages.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.UserContent
{
    public class ValueHandlerModel : PageModel
    {
        readonly private IUserRepos repos;
        readonly private string url= "UserMain";
        public ValueHandlerModel(IUserRepos userRepos)
        {
            repos = userRepos;
        }
        public void OnGet()
        {
        }

   
        public IActionResult OnGetBusyBook(int idBook,int idAccount)
        {
            repos.BusyBook(idBook, idAccount);
            IdBookAccountViewModel bookAccountViewModel = new IdBookAccountViewModel(idBook, idAccount);
            return RedirectToPage("BookPage","Restart" , bookAccountViewModel);
        }

        public IActionResult OnGetTakeOff(int idBook, int idAccount)
        {
            repos.TakeOff(idBook);
            IdBookAccountViewModel bookAccountViewModel = new IdBookAccountViewModel(idBook, idAccount);
            return RedirectToPage("BookPage", "Restart", bookAccountViewModel);
        }

        public IActionResult OnGetMakeComment(Account account, int idBook, string textComment)
        {
            repos.MakeComment(account, idBook, textComment);
            IdBookAccountViewModel bookAccountViewModel = new IdBookAccountViewModel(idBook, account.Id);
            return RedirectToPage("BookPage", "Restart", bookAccountViewModel);
        }
        //public IActionResult OnGetMakeComment(CommentViewModel comment)
        //{
        //    repos.MakeComment(comment.CommentAccount, comment.IdBook, comment.TextComment);
        //    BookAccountViewModel bookAccountViewModel = new BookAccountViewModel(comment.IdBook, comment.CommentAccount.Id);
        //    return RedirectToPage("BookPage", "Restart", bookAccountViewModel);
        //}
    }
}
