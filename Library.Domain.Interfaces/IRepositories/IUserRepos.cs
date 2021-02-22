using Library.Domain.Interfaces.IData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Interfaces.IRepositories
{
    /// <summary>
    /// Интерфес пользовательского репозитория
    /// </summary>
    /// <typeparam name="B">Книга</typeparam>
    /// <typeparam name="A">Аккаунт</typeparam>
    /// <typeparam name="C">Комментарий</typeparam>
    public interface IUserRepos<B,A,C>
    {
        /// <summary>
        /// Вернуть все книги
        /// </summary>
        IEnumerable<B> AllBooks { get; }

        /// <summary>
        /// Забронировать книгу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idBook"></param>
        void BusyBook(int idBook, int idAccount);

        /// <summary>
        /// Снять бронировку
        /// </summary>
        /// <param name="idBook"></param>
        void TakeOff(int idBook);

        /// <summary>
        /// Просмотреть информацию о книге
        /// </summary>
        /// <param name="idBook"></param>
        /// <returns></returns>
        B GetBook(int idBook);

        /// <summary>
        /// Оставить комментарий на книгу
        /// </summary>
        /// <param name="account"></param>
        /// <param name="idBook"></param>
        /// <param name="textComment"></param>
        public void MakeComment(A account, int idBook, string textComment);

        /// <summary>
        /// Прочесть все комментарии, оставленные к данной книге
        /// </summary>
        /// <param name="idBook"></param>
        /// <returns></returns>
        IEnumerable<C> ReadComments(int idBook);

        /// <summary>
        /// Найти книги по автору
        /// </summary>
        /// <param name="author">Автор</param>
        /// <param name="selection">Выборка книг</param>
        /// <returns></returns>
        List<B> FindBookByAuthor(string author, List<IBook> selection = null);

        /// <summary>
        /// Найти книги по жанру
        /// </summary>
        /// <param name="genre">Жанр</param>
        /// <param name="selection">Выборка книг</param>
        /// <returns></returns>
        List<B> FindBookByGenre(string genre, List<IBook> selection = null);

        /// <summary>
        /// Найти книги по издателю
        /// </summary>
        /// <param name="publisher">Издатель</param>
        /// <param name="selection">Выборка книг</param>
        /// <returns></returns>
        List<B> FindBookByPublisher(string publisher, List<IBook> selection = null);

        /// <summary>
        /// Получить информацию о аккаунте
        /// </summary>
        /// <param name="idAccount"></param>
        /// <returns></returns>
        public A MyAccount(int idAccount);
    }
}
