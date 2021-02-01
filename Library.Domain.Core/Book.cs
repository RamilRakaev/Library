using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Library.Domain.Core
{
    public class Book
    {
        public int Id { get; set; }

        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get; set; }

        [MaybeNull]
        /// <summary>
        /// Описание книги
        /// </summary>
        public string Description { get; set; }

        [NotMapped]
        public string ShortDescription 
        { 
            get 
            {
                if (Description != null & Description.Length > 100)
                {
                    return Description.Substring(0, 100);
                }
                return "Краткое описание";
            } 
        }
        /// <summary>
        /// Фото обложки книги
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Занята ли книга
        /// </summary>
        public bool IsBusy { get; set; }

        /// <summary>
        /// Кем была забронирована книга, если никем, возвращает ноль
        /// </summary>
        public int IdAccount { get; set; }

        /// <summary>
        /// Книгу забрали
        /// </summary>
        public bool IsBorrow { get; set; }


        /// <summary>
        /// Если книгу забронировали, то на сколько дней, иначе возвращает 0
        /// </summary>
        public int BookingTime { get; set; }

        /// <summary>
        /// Сколько раз брали книгу
        /// </summary>
        public int BookRequests { get; set; }
    

        /// <summary>
        /// Состояние книги
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Автор книги
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Жанр книги
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Издатель
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Год издания
        /// </summary>
        public ushort PublisherYear { get; set; }
    }
}
