using Library.Domain.Interfaces.IData;
using Library.Domain.Interfaces.IRepositories;
using Library.Infrastructure.Bll.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Mock
{
    public class LibrarianMock : ILibrarianRepos<IBook, IAccount,IHistory>
    {
        public readonly List<IBook> Books;
        public readonly List<IAccount> Accounts;
        public readonly List<IHistory> Histories;

        public LibrarianMock(List<IBook> books, List<IAccount> accounts,List<IHistory> histories)
        {
            Books = books;
            Accounts = accounts;
            Histories = histories;
        }

        public void Dispose()
        {

        }

        #region Информация о книгах
        public IEnumerable<IBook> AllBooks =>Books;

        public IEnumerable<IBook> BusyBooks => Books.Where(b=>b.IsBusy==true);

        public IEnumerable<IBook> FreeBooks => Books.Where(b => b.IsBusy == false);

        public IEnumerable<IBook> GivenBooks => Books.Where(b=>b.IsBorrow==true);

        public IBook GetBook(int idBook) => Books.Where(b => b.Id == idBook).FirstOrDefault();

        public IEnumerable<IBook> BookForTitle(string title) => Books.Where(b => b.Title.StartsWith(title));
        #endregion

        #region Добавить, удалить
        public void AddBook(IBook newBook) => Books.Add(newBook);

        public void RemoveBook(int idBook) => Books.Remove(Books.Where(b => b.Id == idBook).FirstOrDefault());
        #endregion

        #region Аккаунты
        public IEnumerable<IAccount> Users => Accounts.Where(a => a.Rights == "user");

        public IAccount GetAccount(int idAccount) =>Accounts.Where(a => a.Id == idAccount).FirstOrDefault();
        #endregion

        #region Сдача и приёмка
        public void GiveBook(IHistory history)
        {
            Histories.Add(history);
            var book = Books.Where(b => b.Id == history.IdBook).FirstOrDefault();
            book.IsBorrow = true;
        }

        public void InitialAcceptBook(int idBook, int idAccount)
        {
            var history = Histories.
                Where(h => h.IdAccount == idAccount & h.IdBook == idBook).FirstOrDefault();

            history.EndDate = DateTime.Now;
        }

        public void AcceptBook(int idBook, int idAccount, string state = "", bool loss = false)
        {
            var book = GetBook(idBook);
            book.IsBusy = false;
            book.IsBorrow = false;
            book.IdAccount = 0;

            var history = Histories.
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
                    Books.Remove(book);
                }
            }

        }
        #endregion

        #region Штрафы и просроченные сроки
        public void PayOffFine(int idAccount, ulong pay = 0)
        {
            var acc = Accounts.Where(a=>a.Id==idAccount).FirstOrDefault();
                if (pay == 0)
                {
                    acc.Penalty = 0;
                }
                else
                {
                    acc.Penalty -= pay;
                }
            
        }

        public void ChargeFine(int idAccount, ulong penalty) =>
            GetAccount(idAccount).Penalty = penalty;

        public int DeadlineOverdue(int idAccount, int idBook)
        {
            var history = Histories.Where(h => h.IdAccount == idAccount & h.IdBook == idBook).FirstOrDefault();
            if (history != null)
            {
                if (history.BookingTime > history.BookingTimeLib)
                    return history.BookingTime - history.BookingTimeLib;
            }
            return 0;
        }

        public IEnumerable<IHistory> GetHistory(int idBook = 0, int idAccount = 0)
        {
            IEnumerable<IHistory> hist = Histories;
            if (idBook != 0)
                hist = hist.Where(h => h.IdBook == idBook);
            if (idAccount != 0)
                hist = hist.Where(h => h.IdAccount == idAccount);

            return hist;
        }

        #endregion
    }
}
