
using Library.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Infrastructure.Bll.Data;

namespace Library.Infrastructure.Bll.Repositories
{
    public class LibrarianRepos : DisposeRelease, ILibrarianRepos<Book,Account,History>
    {

        public LibrarianRepos(LibraryContext libraryContext)
        {
            db = libraryContext;
        }

        ~LibrarianRepos()
        {
            Dispose(false);
        }

        #region Информация о книгах
        public IEnumerable<Book> AllBooks=>db.Books;

        public IEnumerable<Book> AllBusyBooks=> db.Books.Where(b=>b.IsBusy);
        
        public IEnumerable<Book> FreeBooks=>db.Books.Where(b => !b.IsBusy); 
        
        public IEnumerable<Book> GivenBooks => db.Books.Where(b => b.IsBorrow==true); 
        
        public Book GetBook(int idBook)
        {
            return db.Books.Find(idBook);
        }
        #endregion

        #region Удалить, добавить
        public void AddBook(Book newBook)
        {
            db.Books.Add(newBook);
            db.SaveChanges();
        }

        public void RemoveBook(int idBook)
        {
            db.Books.Remove(db.Books.Find(idBook));
            db.SaveChanges();
        }
        #endregion

        #region Сдача и приёмка
        public void GiveBook(History history)
        {
            db.Histories.Add(history);
            var book = db.Books.Find(history.IdBook);
            book.IsBorrow = true;
            book.BookingTime = history.BookingTimeLib;
            db.SaveChanges();
        }

        public void InitialAcceptBook(int idBook, int idAccount)
        {
            var history = db.Histories.
                Where(h => h.IdAccount == idAccount & h.IdBook == idBook).FirstOrDefault();
            if (history != null)
            {
                history.EndDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void AcceptBook(int idBook, int idAccount, string state = "", bool loss = false)
        {
            var book = db.Books.Find(idBook);
            book.IsBusy = false;
            book.IsBorrow = false;
            book.IdAccount = 0;

            var history = db.Histories.
                Where(h => h.IdAccount == idAccount & h.IdBook == idBook).FirstOrDefault();
            if (history != null)
            {
                if (state != "")
                {
                    book.State = state;
                    history.Damage = true;
                }
                if (loss)
                {
                    history.Loss = true;
                    db.Books.Remove(book);
                }
            }
            db.SaveChanges();
        }
        #endregion

        #region Штрафы и просроченные сроки
        public void ChargeFine(int idAccount, ulong penalty)
        {
            var acc =db.Accounts.Find(idAccount);
            if (acc != null)
            {
                acc.Penalty += penalty;
                db.SaveChanges();
            }
        }

        public void PayOffFine(int idAccount, ulong pay = 0)
        {
            var acc = db.Accounts.Find(idAccount);
            if (acc != null)
            {
                if (pay == 0)
                {
                    acc.Penalty = 0;
                }
                else
                {
                    acc.Penalty -= pay;
                }
                db.SaveChanges();
            }
            
        }

        public int DeadlineOverdue(int idAccount, int idBook)
        {
            var history = db.Histories.Where(h => h.IdAccount == idAccount & h.IdBook == idBook).FirstOrDefault();
            if (history != null)
            {
                if (history.BookingTime > history.BookingTimeLib)
                    return history.BookingTime - history.BookingTimeLib;
            }
            return 0;
        }
        #endregion

        #region Аккаунты
        public IEnumerable<Account> Users
        {
            get
            {
                var accounts= db.Accounts.
                    Where(a => a.Rights == "user");
                foreach (Account account in accounts)
                    account.Password = "";
                return accounts;
            }
        }

        public Account GetAccount(int id)
        {
            var account= db.Accounts.Find(id);
            if(account!=null)
            account.Password = "";
            return account;
        }
        #endregion
    }
}
