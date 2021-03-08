using Library.Domain.Interfaces.IData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Interfaces.IRepositories
{
    /// <summary>
    /// Интерфейс репозитория библиотекаря
    /// </summary>
    /// <typeparam name="B">Книга</typeparam>
    /// <typeparam name="A">Аккаунт</typeparam>
    public interface ILibrarianRepos<B,A,H>: IDisposable where B:IBook where A:IAccount where H:IHistory
    {
        /// <summary>
        /// Все книги
        /// </summary>
        IEnumerable<B> AllBooks { get; }

        /// <summary>
        /// Забронированные книги
        /// </summary>
        IEnumerable<B> BusyBooks { get; }

        /// <summary>
        /// Свободные книги
        /// </summary>
        IEnumerable<B> FreeBooks { get; }

        /// <summary>
        /// Отданные книги
        /// </summary>
        IEnumerable<B> GivenBooks { get; }

        /// <summary>
        /// Добавить книгу
        /// </summary>
        /// <param name="newBook"></param>
        void AddBook(B newBook);

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
        void GiveBook(H history);

        /// <summary>
        /// Найти книгу по первым буквам названия
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        IEnumerable<B> BookForTitle(string title);

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
        B GetBook(int idBook);

        /// <summary>
        /// Обычные пользователи
        /// </summary>
        IEnumerable<A> Users { get; }

        /// <summary>
        /// Получить информацию о аккаунте
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        A GetAccount(int id);

        /// <summary>
        /// Вернуть историю бронирований
        /// </summary>
        /// <param name="idAccount"></param>
        /// <param name="idBook"></param>
        /// <returns></returns>
        IEnumerable<H> GetHistory(int idBook = 0, int idAccount=0);
    }
}
