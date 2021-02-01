using System;
using System.Collections.Generic;
using System.Text;
using Library.Domain.Core;

namespace Library.Domain.Interfaces
{
    public interface IAdminRepos:IDisposable
    {
        /// <summary>
        /// Пользователи с особыми правами
        /// </summary>
        IEnumerable<Account> SuperUsers { get; }

        /// <summary>
        /// Обычные пользователи
        /// </summary>
        IEnumerable<Account> Users { get; }

        /// <summary>
        /// Найти аккаунт по паролю и имени
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        Account FindByPassword(string password, string name);


        /// <summary>
        /// Получить информацию о аккаунте
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Account GetAccount(int id);

        /// <summary>
        /// Изменить пароль
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPassword"></param>
        void ChangePassword(int id, string newPassword);

        /// <summary>
        /// Добавить аккаунт
        /// </summary>
        /// <param name="account"></param>
        void AddAccount(Account account);

        /// <summary>
        /// Удалить аккаунт
        /// </summary>
        /// <param name="idAccount"></param>
        void RemoveAccount(int idAccount);
    }
}
