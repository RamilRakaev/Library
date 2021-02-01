using Library.Domain.Core;
using System;

namespace Library.Services.Interfaces
{
    public interface INovelties
    {
        /// <summary>
        /// Отправка по почте информации о новых книгах определённому пользователю
        /// </summary>
        /// <param name="message"></param>
        /// <param name="account"></param>
        /// <param name="books"></param>
        void SendMessageAboutBooks( Account account, Book[] books);

        /// <summary>
        /// Отправка сообщения о новом пароле по почте
        /// </summary>
        /// <param name="password"></param>
        /// <param name="account"></param>
        void SendMessage(string password, Account account);
    }
}
