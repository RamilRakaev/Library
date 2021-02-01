using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.LibrarianContent
{
    public class ValueHandlerModel : PageModel
    {
        readonly private ILibrarianRepos repos;
        IWebHostEnvironment appEnvironment;
        public ValueHandlerModel(ILibrarianRepos librarianRepos, IWebHostEnvironment _appEnvironment)
        {
            repos = librarianRepos;
            appEnvironment = _appEnvironment;
        }

        public void OnGet()
        {
        }

        public IActionResult OnGetRemoveBook(int idBook)
        {
            var book = repos.GetBook(idBook);
            FileInfo fileInfo = new FileInfo(appEnvironment.WebRootPath + "/img/"+book.ImagePath);
            fileInfo.Delete();
            repos.RemoveBook(idBook);
            return RedirectToPage("LibrarianMain");
        }
    }
}
