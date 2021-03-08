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

        #region Переменные по умолчанию
        private static Account DefaultAccount { 
            get 
            { 
                return new Account() {Id=0, Name="",Rights= "user" }; 
            }
        }
        private static Book DefaultBook { 
            get 
            { 
                return new Book() {Id=0, Title="", Author= "", Publisher="", Genre="" }; 
            }
        }

        #endregion

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
            if (selection != null)
            {
                return selection.Where(b => b.Author == author).ToList();
            }
            else
            {
                return db.Books.Where(b => b.Author == author).ToList();
            }
        }

        public List<Book> FindBookByGenre(string genre, List<Book> selection = null)
        {
            if (selection != null)
            {
                return selection.Where(b => b.Genre == genre).ToList();
            }
            else
            {
                return db.Books.Where(b => b.Genre == genre).ToList();
            }
        }

        public List<Book> FindBookByPublisher(string publisher, List<Book> selection = null)
        {
            if (selection != null)
            {
                    return selection.Where(b => b.Publisher == publisher).ToList();
            }
            else
            {
                return db.Books.Where(b => b.Publisher == publisher).ToList();
            }
        }

        public IEnumerable<Book> BookForTitle(string title) => db.Books.Where(b => b.Title.StartsWith(title));
        #endregion

        #region Книги и аккаунты
        public IEnumerable<Book> AllBooks => db.Books;

        public Book GetBook(int idBook)
        {
            return db.Books.Where(b=>b.Id==idBook).ToList().DefaultIfEmpty(DefaultBook).First();
        }

        public Account MyAccount(string password, string name)
        {
            var account = db.Accounts.Where(b=>b.Password == password & 
            b.Name==name & b.Rights=="user").ToList().DefaultIfEmpty(DefaultAccount).First();
            
            return account;
        }
        #endregion
    }
}
