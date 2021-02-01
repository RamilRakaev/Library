using Library.Domain.Core;
using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Data
{
    public class LibrarianRepos : DisposeRelease, ILibrarianRepos
    {

        public LibrarianRepos(LibraryContext libraryContext)
        {
            db = libraryContext;
        }
        public IEnumerable<Book> AllBooks
        {
            get
            {
                return db.Books;
            }
        }

        public IEnumerable<Book> AllBusyBooks
        {
            get { return db.Books.Where(b=>b.IsBusy); }
        }

        public IEnumerable<Book> FreeBooks
        {
            get { return db.Books.Where(b => !b.IsBusy); }
        }

        public IEnumerable<Book> GivenBooks
        {
            get { return db.Books.Where(b => b.IsBorrow); }
        }

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

        public void GiveBook(int idBook, int idAccount, int days = 0)
        {
            db.Histories.Add(new History(idAccount, idBook));
            var book = db.Books.Find(idBook);
            book.IsBorrow = true;
            book.BookingTime = days;
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

        public Book GetBook(int idBook)
        {
            return db.Books.Find(idBook);
        }

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

    }
}
