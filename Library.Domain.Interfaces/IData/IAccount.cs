using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Interfaces.IData
{
    public interface IAccount
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пароль 
        /// </summary>
        public string Password { get; set; } 

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; } 

        /// <summary>
        /// Права доступа
        /// </summary>
        public string Rights { get; set; }

        /// <summary>
        /// Занятых книг
        /// </summary>
        public sbyte BusyBooks { get; set; }

        /// <summary>
        /// Штраф
        /// </summary>
        public ulong Penalty { get; set; }
    }
}
