using Library.Domain.Interfaces.IData;
using Library.Domain.Interfaces.IRepositories;
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

        public LibrarianMock(List<IBook> books, List<IAccount> accounts)
        {
            Books = books;
            Accounts = accounts;
        }

        public void Dispose()
        {

        }

        #region Информация о книгах
        public IEnumerable<IBook> AllBooks =>Books;

        public IEnumerable<IBook> AllBusyBooks => Books.Where(b=>b.IsBusy==true);

        public IEnumerable<IBook> FreeBooks => Books.Where(b => b.IsBusy == false);

        public IEnumerable<IBook> GivenBooks => Books.Where(b=>b.IsBorrow==true);

        public IBook GetBook(int idBook) => Books.Where(b => b.Id == idBook).FirstOrDefault();
        #endregion

        #region Добавить, удалить
        public void AddBook(IBook newBook) => Books.Add(newBook);

        public void RemoveBook(int idBook) => Books.Remove(Books.Where(b => b.Id == idBook).FirstOrDefault());
        #endregion

        #region Аккаунты
        public IEnumerable<IAccount> Users => Accounts.Where(a => a.Rights == "users");

        public IAccount GetAccount(int idAccount) =>Accounts.Where(a => a.Id == idAccount).FirstOrDefault();
        #endregion

        #region Сдача и приёмка
        public void GiveBook(IHistory history)
        {
            Histories.Add(history);
            var book = Books.Where(b => b.Id == history.IdBook).FirstOrDefault();
            book.IsBorrow = true;
            book.BookingTime = history.BookingTimeLib;
        }

        public void InitialAcceptBook(int idBook, int idAccount)
        {
            var history = Histories.
                Where(h => h.IdAccount == idAccount & h.IdBook == idBook).FirstOrDefault();

            history.EndDate = DateTime.Now;
        }

        public void AcceptBook(int idBook, int idAccount, string state = "", bool loss = false)
        {
            var history = Histories.Where(h => h.IdAccount == idAccount & h.IdBook == idBook).FirstOrDefault();
            if (state != null)
                history.Damage = true;
            history.Loss = loss;

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
        #endregion
    }
}
