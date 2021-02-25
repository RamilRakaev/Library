using Library.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Library.Infrastructure.Bll.Data;

namespace Library.Infrastructure.Bll.Repositories
{
    public class AdminRepos : DisposeRelease, IAdminRepos<Account>
    {

        public AdminRepos(LibraryContext context)
        {
            db = context;
        }

        ~AdminRepos()
        {
            Dispose(false);
        }

        #region Добавить удалить аккаунт
        public void AddAccount(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
        }

        public void RemoveAccount(int idAccount)
        {
            db.Accounts.Remove(db.Accounts.Find(idAccount));
            db.SaveChanges();
        }
        #endregion

        #region Управление паролями
        public void ChangePassword(int id, string newPassword)
        {
            var acc = db.Accounts.Find(id);
            if (acc != null)
            {
                acc.Password = newPassword;
            }

            db.SaveChanges();
        }

        public Account FindByPassword(string password, string name)
        {
            var account = db.Accounts.Where(a => a.Password == password & a.Name == name).FirstOrDefault();
            if (account != null)
                return account;
            return new Account();
        }
        #endregion

        #region Информация о аккаунтах
        public Account GetAccount(int id)
        {
            return db.Accounts.Find(id);
        }

        public IEnumerable<Account> SuperUsers 
        { 
            get 
            { 
                return db.Accounts.
                    Where(a=>a.Rights == "admin" | a.Rights == "librarian"); 
            }
        }

        public IEnumerable<Account> Users
        {
            get
            {
                return db.Accounts.
                    Where(a => a.Rights == "user").AsEnumerable();
            }
        }
        #endregion

    }
}
