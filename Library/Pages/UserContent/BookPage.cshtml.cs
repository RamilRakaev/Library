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
    public class BookPageModel : PageModel
    {
        readonly private IUserRepos repos;
        public Book UserBook { get; set; }
        [BindProperty]
        public int idAccount { get; set; }
        public Account UserAccount { get; set; }
        public string TextComment { get; set; }
        public List<Comment> Comments { get; set; }

        public BookPageModel(IUserRepos userRepos)
        {
            repos = userRepos;
        }
        public void OnGet(int _idBook,int _idAccount)
        {
            Comments = repos.ReadComments(_idBook).ToList();
            UserBook = repos.GetBook(_idBook);
            idAccount = _idAccount;
            UserAccount = repos.MyAccount(_idAccount);
        }

        /// <summary>
        /// Перезапуск страницы                                                                                                                                                                  
        /// </summary>
        /// <param name="bookAccountViewModel"></param>
        public void OnGetRestart(IdBookAccountViewModel bookAccountViewModel )
        {
            Comments = repos.ReadComments(bookAccountViewModel.IdBook).ToList();
            UserBook = repos.GetBook(bookAccountViewModel.IdBook);
            idAccount =bookAccountViewModel.IdAccount;
            UserAccount = repos.MyAccount(idAccount);
        }

        public IActionResult OnPost(int idAccount,string nameAccount, int idBook, string textComment)
        {
            UserAccount = new Account() { Id = idAccount, Name = nameAccount };
            
            repos.MakeComment(UserAccount, idBook, textComment);
            IdBookAccountViewModel bookAccountViewModel = new IdBookAccountViewModel(idBook, idAccount);
            return RedirectToPage("BookPage", "Restart", bookAccountViewModel);
        }

    }
}
