using Library.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Infrastructure.Bll.Data;

namespace Library.Infrastructure.Bll.Repositories
{
    public class UserRepos:DisposeRelease, IUserRepos<Book,Account,Comment>
    {
        public UserRepos(LibraryContext context)
        {
            db = context;
        }

        ~UserRepos()
        {
            Dispose(false);
        }

        #region Бронировка
        public void BusyBook(int idBook, int idAccount)
        {
            var book = db.Books.Find(idBook);
            book.IsBusy = true;
            book.IdAccount = idAccount;
            var account = db.Accounts.Find(book.IdAccount);
            if (account != null)
                db.Accounts.Find(idAccount).BusyBooks++;
            db.SaveChanges();
        }

        public void TakeOff(int idBook)
        {
            var book = db.Books.Find(idBook);
            book.IsBusy = false;
            book.IsBorrow = false;
            var account = db.Accounts.Find(book.IdAccount);
            if(account!=null)
                account.BusyBooks--;
            book.IdAccount = 0;
            db.SaveChanges();
        }
        #endregion

        #region Комментарии
        public void MakeComment(Comment comment)
        {
            db.Comments.Add(comment);
            db.SaveChanges();
        }

        public IEnumerable<Comment>ReadComments(int idBook)=>
            db.Comments.Where(c => c.IdBook == idBook);
        #endregion

        #region Поиск книги
        public List<Book> FindBookByAuthor(string author, List<Book> selection = null)
        {
            List<Book> books = new List<Book>();
            if (selection != null & selection.Count() != 0)
            {
                books.AddRange(selection.Where(b => b.Author == author).ToList());
            }
            else
            {
                books.AddRange(db.Books.Where(b => b.Publisher == author).ToList());
            }
            return books;
        }

        public List<Book> FindBookByGenre(string genre, List<Book> selection = null)
        {
            List<Book> books = new List<Book>();
            if (selection != null & selection.Count() != 0)
            {
                    books.AddRange(selection.Where(b => b.Genre == genre).ToList());
            }
            else
            {
                books.AddRange(db.Books.Where(b => b.Genre == genre).ToList());
            }
            return books;
        }

        public List<Book> FindBookByPublisher(string publisher, List<Book> selection = null)
        {
            List<Book> books = new List<Book>();
            if (selection != null)
            {
                if (selection.Count() != 0 & selection.Count() != 0)
                    books.AddRange(selection.Where(b => b.Publisher == publisher).ToList());
            }
            else
            {
                books.AddRange(db.Books.Where(b => b.Publisher == publisher).ToList());
            }
            return books;
        }
        #endregion

        #region Книги и аккаунты
        public IEnumerable<Book> AllBooks
        {
            get
            {
                var books = db.Books;
                return books;
            }
        }

        public Book GetBook(int idBook)
        {
            return db.Books.Find(idBook);
        }

        public Account MyAccount(int idAccount)
        {
            var book = db.Accounts.Find(idAccount);
            if (book != null)
            {
                if (book.Rights == "user")
                    return book;
            }
            return new Account() { Id = 0 };
        }
        #endregion
    }
}
