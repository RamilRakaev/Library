using Library.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Pages.ViewModel
{
    public class CommentViewModel
    {
        public int IdBook { get; set; }
        public Account CommentAccount { get; set; }
        public string TextComment { get; set; }
        public CommentViewModel(int idBook, Account commentAccount, string textComment)
        {
            IdBook = idBook;
            CommentAccount = commentAccount;
            TextComment = textComment;
        }
    }
}
