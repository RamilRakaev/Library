using Library.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Interfaces
{
    public interface IUserRepos
    {
        /// <summary>
        /// Вернуть все книги
        /// </summary>
        IEnumerable<Book> AllBooks { get; }

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
        Book GetBook(int idBook);

        /// <summary>
        /// Оставить комментарий на книгу
        /// </summary>
        /// <param name="account"></param>
        /// <param name="idBook"></param>
        /// <param name="textComment"></param>
        public void MakeComment(Account account, int idBook, string textComment);

        /// <summary>
        /// Прочесть все комментарии, оставленные к данной книге
        /// </summary>
        /// <param name="idBook"></param>
        /// <returns></returns>
        IEnumerable<Comment> ReadComments(int idBook);

        /// <summary>
        /// Найти книги по автору
        /// </summary>
        /// <param name="author">Автор</param>
        /// <param name="selection">Выборка книг</param>
        /// <returns></returns>
        List<Book> FindBookByAuthor(string author, List<Book> selection = null);

        /// <summary>
        /// Найти книги по жанру
        /// </summary>
        /// <param name="genre">Жанр</param>
        /// <param name="selection">Выборка книг</param>
        /// <returns></returns>
        List<Book> FindBookByGenre(string genre, List<Book> selection = null);

        /// <summary>
        /// Найти книги по издателю
        /// </summary>
        /// <param name="publisher">Издатель</param>
        /// <param name="selection">Выборка книг</param>
        /// <returns></returns>
        List<Book> FindBookByPublisher(string publisher, List<Book> selection = null);

        /// <summary>
        /// Получить информацию о аккаунте
        /// </summary>
        /// <param name="idAccount"></param>
        /// <returns></returns>
        public Account MyAccount(int idAccount);
    }
}
