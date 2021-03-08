using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Domain.Interfaces.IData;

namespace Library.Infrastructure.Bll.Data
{
    public class Comment:IComment
    {

        public Comment()
        {

        }

        public Comment(IAccount account, int idBook, string textComment)
        {
            IdAccount = account.Id;
            Name = account.Name;
            IdBook = idBook;
            TextComment = textComment;
            Date = DateTime.Now;
        }

        public int Id { get; set; }

        public int IdAccount { get; set; }
        public int IdBook { get; set; }

        public string Name { get; set; }

        public string TextComment { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return Name + ": " + TextComment;
        }
    }
}
