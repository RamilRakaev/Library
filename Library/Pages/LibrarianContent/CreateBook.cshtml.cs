using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.Core;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.LibrarianContent
{
    public class CreateBookModel : PageModel
    {
        public Book Book { get; set; }
        readonly private ILibrarianRepos repos;
        IWebHostEnvironment appEnvironment;
        public CreateBookModel(ILibrarianRepos librarianRepos, IWebHostEnvironment _appEnvironment)
        {
            repos = librarianRepos;
            appEnvironment = _appEnvironment;
        }

        public void OnGet()
        {
            Book = new Book();
        }

        public async Task<IActionResult> OnPost(Book book,IFormFile uploadedImage)
        {
            Book newBook = book;
            if (uploadedImage != null)
            {
                string path = "/img/" + uploadedImage.FileName;
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedImage.CopyToAsync(fileStream);
                }
                newBook.ImagePath = uploadedImage.FileName;
            }
            repos.AddBook(newBook);
            return RedirectToPage("LibrarianMain");
        }
    }
}
