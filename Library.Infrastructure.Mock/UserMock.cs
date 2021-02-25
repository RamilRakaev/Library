using Library.Domain.Interfaces.IData;
using Library.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Mock
{
    public class UserMock : IUserRepos<IBook, IAccount, IComment>
    {
        public readonly List<IBook> Books;
        public readonly List<IAccount> Accounts;
        public readonly List<IComment> Comments;

        public UserMock(List<IBook> books, List<IAccount> accounts, List<IComment> comments)
        {
            Books = books;
            Accounts = accounts;
            Comments = comments;
        }

        public void Dispose()
        {

        }

        #region Бронировка
        public void BusyBook(int idBook, int idAccount) 
        {
            var book = Books.Where(b => b.Id == idBook).FirstOrDefault();
            book.IsBusy = true;
            book.IdAccount = idAccount;
        }

        public void TakeOff(int idBook) =>
            Books.Where(b => b.Id == idBook).FirstOrDefault().IsBusy = false;
        #endregion

        #region Поиск книги
        public List<IBook> FindBookByAuthor(string author, List<IBook> selection = null)
        {
            if (selection == null)
                selection= Books;

                return selection.Where(b => b.Author == author).ToList();
        }

        public List<IBook> FindBookByGenre(string genre, List<IBook> selection = null)
        {
            if (selection == null)
                selection = Books;

            return selection.Where(b => b.Genre == genre).ToList();
        }

        public List<IBook> FindBookByPublisher(string publisher, List<IBook> selection = null)
        {
            if (selection == null)
                selection = Books;

            return selection.Where(b => b.Publisher == publisher).ToList();
        }
        #endregion

        #region Комментарии
        public void MakeComment(IComment comment) => Comments.Add(comment);

        public IEnumerable<IComment> ReadComments(int idBook) => Comments.Where(c => c.Id == idBook);
        #endregion

        #region Книги и аккаунты
        public IEnumerable<IBook> AllBooks => Books;

        public IBook GetBook(int idBook) => Books.Where(b => b.Id == idBook).FirstOrDefault();

        public IAccount MyAccount(int idAccount) => Accounts.Where(a => a.Id == idAccount).FirstOrDefault();
        #endregion
    }
}
