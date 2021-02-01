using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.Core;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.UserContent
{
    public class UserMainModel : PageModel
    {
        readonly private IUserRepos repos;
        public List<Book> Books { get; set; }
        [BindProperty]
        public int IdAccount { get; set; }

        //��������� ���������� ��� ������ ���������� ����
        public List<string> Genres { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Publishers { get; set; }

        public UserMainModel(IUserRepos userRepos)
        {
            repos = userRepos;
        }

        public void OnGet()
        {

            IdAccount = (int)TempData["id"];
            TempData["id"] = IdAccount;
            Books= repos.AllBooks.ToList();
            ParameterDistributionGenres(); 
            ParameterDistributionAuthors();
            ParameterDistributionPublishers();
        }

        public void OnPost(string genre,string author,string publisher, int idAccount)
        {
            IdAccount = idAccount;
            Books = repos.AllBooks.ToList();
            ParameterDistributionGenres();
            ParameterDistributionAuthors();
            ParameterDistributionPublishers();
            Books = new List<Book>();
            if(genre!= "�� ������")
            {
                Books = repos.FindBookByGenre(genre, Books);
            }
            if(author != "�� ������")
            {
                Books = repos.FindBookByAuthor(author, Books);
            }
            if(publisher != "�� ������")
            {
                Books = repos.FindBookByPublisher(publisher, Books);
            }
        }

        /// <summary>
        /// ����� ���� ��������� ������ ����
        /// </summary>
        private void ParameterDistributionGenres()
        {
            Genres = new List<string>() { "�� ������" };
            foreach(Book book in Books)
            {
                if (!Genres.Contains(book.Genre))
                {
                    string genre;
                    if (book.Genre.Length > 20)
                        genre = book.Genre.Substring(0, 20) + "...";
                    else
                        genre = book.Genre;
                    Genres.Add(genre);
                }
            }
        }

        /// <summary>
        /// ����� ���� ��������� �������
        /// </summary>
        private void ParameterDistributionAuthors()
        {
            Authors = new List<string>() {"�� ������" };
            foreach(Book book in Books)
            {
                if (!Authors.Contains(book.Author))
                {
                    string author;
                    if (book.Author.Length > 25)
                        author = book.Author.Substring(0, 25) + "...";
                    else
                        author = book.Author;
                    Authors.Add(author);
                }
            }
        }

        /// <summary>
        /// ����� ���� ��������� ���������
        /// </summary>
        private void ParameterDistributionPublishers()
        {
            Publishers = new List<string>() { "�� ������" };
            foreach(Book book in Books)
            {
                if (!Publishers.Contains(book.Publisher))
                {
                    string publisher;
                    if (book.Publisher.Length > 20)
                        publisher = book.Publisher.Substring(0, 20) + "...";
                    else
                        publisher = book.Publisher;
                    Publishers.Add(publisher);
                }
            }
        }
    }
}
