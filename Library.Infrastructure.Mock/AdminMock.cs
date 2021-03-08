using Library.Domain.Interfaces.IData;
using Library.Domain.Interfaces.IRepositories;
using Library.Infrastructure.Bll.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Infrastructure.Mock
{
    public class AdminMock : IAdminRepos<IAccount>
    {
        private readonly List<IAccount> Accounts;

        public AdminMock(List<IAccount> accounts)
        {
            Accounts = accounts;
        }

        public void Dispose()
        {

        }

        #region Добавить удалить аккаунт
        public void AddAccount(IAccount account)=>Accounts.Add(account);

        public void RemoveAccount(int idAccount) => Accounts.Remove(Accounts.Where(a => a.Id == idAccount).FirstOrDefault());
        #endregion

        #region Управление паролями
        public void ChangePassword(int id, string newPassword)=>
            Accounts.Where(a => a.Id == id).FirstOrDefault().Password = newPassword;

        
        public IAccount FindByPassword(string password, string name) => 
            Accounts.Where(a => a.Password == password & a.Name == name).FirstOrDefault();
        #endregion

        #region Информация о аккаунтах
        public IAccount GetAccount(int id) => Accounts.Where(a => a.Id == id).FirstOrDefault();

        public IEnumerable<IAccount> SuperUsers => Accounts.
                    Where(a => a.Rights == "admin" | a.Rights == "librarian");

        public IEnumerable<IAccount> Users => Accounts.Where(a => a.Rights == "user");
        #endregion
    }
}
