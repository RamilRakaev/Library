using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Interfaces.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="A">Аккаунт</typeparam>
    public interface IAdminRepos<A>: IDisposable
    {
        #region Поиск
        /// <summary>
        /// Пользователи с особыми правами
        /// </summary>
        IEnumerable<A> SuperUsers { get; }

        /// <summary>
        /// Обычные пользователи
        /// </summary>
        IEnumerable<A> Users { get; }

        /// <summary>
        /// Найти аккаунт по паролю и имени
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        A FindByPassword(string password, string name);


        /// <summary>
        /// Получить информацию о аккаунте
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        A GetAccount(int id);
        #endregion

        #region Обработка данных
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
        void AddAccount(A account);

        /// <summary>
        /// Удалить аккаунт
        /// </summary>
        /// <param name="idAccount"></param>
        void RemoveAccount(int idAccount);
        #endregion
    }
}
