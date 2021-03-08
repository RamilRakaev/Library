using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Library.Domain.Interfaces.IData
{
    public interface IBook
    {
        
                public int Id { get; set; }

                /// <summary>
                /// Название книги
                /// </summary>
                public string Title { get; set; }

                /// <summary>
                /// Описание книги
                /// </summary>
                [MaybeNull]
                public string Description { get; set; }

                /// <summary>
                /// Краткое описание книги
                /// </summary>
                [NotMapped]
                public string ShortDescription{get;}
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
