using Library.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Interfaces
{
    public interface ILibrarianRepos: IDisposable
    {
        /// <summary>
        /// Все книги
        /// </summary>
        IEnumerable<Book> AllBooks { get; }

        /// <summary>
        /// Все забронированные книги
        /// </summary>
        IEnumerable<Book> AllBusyBooks { get; }

        /// <summary>
        /// Все свободные книги
        /// </summary>
        IEnumerable<Book> FreeBooks { get; }

        /// <summary>
        /// Все отданные книги
        /// </summary>
        IEnumerable<Book> GivenBooks { get; }

        /// <summary>
        /// Добавить книгу
        /// </summary>
        /// <param name="newBook"></param>
        void AddBook(Book newBook);

        /// <summary>
        /// Удалить книгу
        /// </summary>
        /// <param name="idBook"></param>
        void RemoveBook(int idBook);

        /// <summary>
        /// Отметить книгу как отданную
        /// </summary>
        /// <param name="idBook">Идентификатор книги</param>
        /// <param name="days">На сколько дней была отдана книга</param>
        void GiveBook(int idBook, int idAccount, int days=0);

        /// <summary>
        /// Обозначить время приёмки книги
        /// </summary>
        /// <param name="idBook"></param>
        /// <param name="idAccount"></param>
        void InitialAcceptBook(int idBook, int idAccount);

        /// <summary>
        /// Принять книгу и отметить как свободную
        /// </summary>
        /// <param name="idBook"></param>
        /// <param name="state">Состояние книги,если оно изменилось</param>
        void AcceptBook(int idBook, int idAccount, string state = "", bool loss = false);

        /// <summary>
        /// Время на которое срок сдачи был просрочен, если не просрочен, возвращает ноль
        /// </summary>
        /// <param name="idAccount"></param>
        /// <param name="idBook"></param>
        /// <returns></returns>
        int DeadlineOverdue(int idAccount, int idBook);

        /// <summary>
        /// Начислить штраф за порчу, потерю или сорванный срок
        /// </summary>
        /// <param name="idBook"></param>
        void ChargeFine(int idAccount, ulong penalty);

        /// <summary>
        /// Погасить штраф на определённую сумму
        /// </summary>
        /// <param name="idAccount"></param>
        /// <param name="pay">Сумма платы, если равна нулю, то штраф погашается полностью</param>
        void PayOffFine(int idAccount, ulong pay =0);

        /// <summary>
        /// Просмотреть информацию о книге
        /// </summary>
        /// <param name="idBook"></param>
        /// <returns></returns>
        Book GetBook(int idBook);

        /// <summary>
        /// Обычные пользователи
        /// </summary>
        IEnumerable<Account> Users { get; }

        /// <summary>
        /// Получить информацию о аккаунте
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Account GetAccount(int id);
    }
}
