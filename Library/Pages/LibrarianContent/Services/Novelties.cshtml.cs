using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.Core;
using Library.Domain.Interfaces;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.LibrarianContent.Services
{
    public class NoveltiesModel : PageModel
    {
        [BindProperty]
        public bool[] BooksBools { get; set; }
        [BindProperty]
        public Book[] Books { get; set; }
        public List<string> Names { get; set; }

        readonly private ILibrarianRepos repos;
        readonly private INovelties novel;
        public NoveltiesModel(ILibrarianRepos librarianRepos, INovelties novelties)
        {
            repos = librarianRepos;
            novel = novelties;
        }

        public void OnGet()
        {
            Books = repos.AllBooks.ToArray();
            BooksBools = new bool[Books.Length];
            Names = new List<string>();
            foreach(Account account in repos.Users)
            {
                Names.Add(account.Name);
            }
        }

        public IActionResult OnPost(string name)
        {
            Books = repos.AllBooks.ToArray();
            Account account = repos.Users.Where(u => u.Name == name).First();
            if (account != null)
            { 
                List<Book> sendBooks = new List<Book>();
                if (Books.Length == BooksBools.Length)
                    for (int i = 0; i < Books.Length; i++)
                    {
                        if (BooksBools[i])
                        {
                            sendBooks.Add(Books[i]);
                        }
                    }
                novel.SendMessageAboutBooks(account, sendBooks.ToArray()); 
            }
            return RedirectToPage("../LibrarianMain");
        }
    }
}
