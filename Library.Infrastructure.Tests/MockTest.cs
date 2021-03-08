using Library.Domain.Interfaces.IData;
using Library.Infrastructure.Bll.Data;
using Library.Infrastructure.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Tests
{
    [TestClass()]
    public class MockTest
    {
        static IEnumerable<IBook> books = new List<Book>() {
                new Book() {Id=1,Title="title1",Author="author1",Publisher="publisher1",
                    Genre="genre1", PublisherYear=2001 },
                new Book(){Id=2,Title="title2",Author="author2",Publisher="publisher2",
                    Genre="genre2", PublisherYear=2002,IsBusy=true,IsBorrow=true}
            };
        static IEnumerable<IAccount> accounts = new List<Account>()
            {
                new Account()
                {
                    Id=1,Name="Jonathan",Rights="user"

                },
            new Account() { Id=2, Name = "name1",Password="11", Email="email1", Rights = "user" },
            new Account() { Id=3,Name = "name2",Password="22", Email="email2", Rights = "user" },
            new Account() { Id=4,Name = "name3", Password = "33", Email = "email3", Rights = "admin" }
            };
        static IEnumerable<IComment> comments = new List<Comment>();
        static IEnumerable<IHistory> histories = new List<History>() 
        { 
            new History(2, 1, 2) { StartDate = new DateTime(2021, 1, 1), EndDate = new DateTime(2021, 1, 3), BookingTimeLib = 1 } 
        };
        

        [TestMethod()]
        public void UserMockTest()
        {
            UserMock userMock = new UserMock(books.ToList(), accounts.ToList(), comments.ToList());
            //Бронировка
            userMock.BusyBook(2, 1);
            Assert.IsTrue(userMock.GetBook(2).IsBusy);
            Assert.AreEqual(userMock.GetBook(2).IdAccount, 1);

            userMock.TakeOff(2);
            Assert.IsFalse(userMock.GetBook(2).IsBusy);

            //Поиск книг
            Assert.AreEqual(userMock.FindBookByAuthor("author1").First().Id, 1);
            Assert.AreEqual(userMock.FindBookByPublisher("publisher2").First().Id, 2);
            Assert.AreEqual(userMock.FindBookByGenre("genre1").First().Id, 1);

            //Комментарии
            Comment comment1 = new Comment() { IdBook = 1, TextComment = "comment1" };
            Comment comment2 = new Comment() { IdBook = 1, TextComment = "comment2" };
            userMock.MakeComment(comment1);
            userMock.MakeComment(comment2);
            Assert.AreEqual(userMock.ReadComments(1).ElementAt(0).TextComment, "comment1");

            //Книги и аккаунты
            Assert.AreEqual(userMock.AllBooks.Count(), 2);

            Assert.IsNull(userMock.GetBook(100));
            Assert.AreEqual(userMock.GetBook(2).Title, "title2");

            Assert.AreEqual(userMock.MyAccount("22", "name2").Email, "email2");
        }

        /// <summary>
        /// Тест репозитория LibrarianMock
        /// </summary>
        [TestMethod()]
        public void LibrarianMockTest()
        {
            LibrarianMock librarianRepos = new LibrarianMock(books.ToList(),accounts.ToList(),histories.ToList());

            //Информация о книгах
            int Id = librarianRepos.AllBooks.ElementAt(1).Id;
            Assert.IsTrue(librarianRepos.BusyBooks.ElementAt(0).IsBusy);
            Assert.IsFalse(librarianRepos.FreeBooks.ElementAt(0).IsBusy);
            Assert.IsTrue(librarianRepos.GivenBooks.ElementAt(0).IsBorrow);
            Assert.AreEqual(librarianRepos.GetBook(Id).Title, "title2");

            //Удалить, добавить
            librarianRepos.AddBook(new Book() { Title = "12345", Author = "author3", Publisher = "publisher3", Genre = "genre3", IsBusy = true, IsBorrow = true,Id=10 });
            var book = librarianRepos.BookForTitle("12").First();
            Assert.AreEqual(book.Author, "author3");

            librarianRepos.RemoveBook(book.Id);
            Assert.IsNull(librarianRepos.BookForTitle("12").FirstOrDefault());
            Assert.IsNotNull(librarianRepos.BookForTitle("123"));

            //Сдача и приёмка
            librarianRepos.GiveBook(new History(1, Id, 20));
            Assert.IsTrue(librarianRepos.GetBook(Id).IsBorrow);

            librarianRepos.InitialAcceptBook(Id, 1);
            librarianRepos.AcceptBook(Id, 1, "Порвано");

            Assert.AreEqual(librarianRepos.GetBook(Id).State, "Порвано");
            Assert.IsTrue(librarianRepos.GetHistory(0, 1).FirstOrDefault().Damage);
            Assert.AreEqual(librarianRepos.GetHistory(0, 1).First().BookingTimeLib, 20);

            //Штрафы, просроченные сроки, aккаунты и история
            var users = librarianRepos.Users;
            Assert.IsTrue(users.Count() > 0);
            int accountId = users.ElementAt(0).Id;
            librarianRepos.ChargeFine(accountId, 200);
            Assert.IsTrue(librarianRepos.GetAccount(accountId).Penalty == 200);

            Assert.IsTrue(librarianRepos.DeadlineOverdue(2, 1) == 1);

            librarianRepos.PayOffFine(accountId, 100);
            Assert.IsTrue(librarianRepos.GetAccount(accountId).Penalty == 100);
        }

        /// <summary>
        /// Тест репозитория AdminMock
        /// </summary>
        [TestMethod()]
        public void AdminReposTest()
        {

            AdminMock adminRepos = new AdminMock(accounts.ToList());

            //Информация о аккаунтах
            var user = adminRepos.Users.First();
            Assert.IsTrue(user.Rights == "user");
            Assert.IsTrue(adminRepos.SuperUsers.First().Rights == "admin");
            Assert.IsTrue(adminRepos.GetAccount(user.Id).Name == user.Name);

            //Пароли
            int Id = adminRepos.Users.ElementAt(0).Id;
            adminRepos.ChangePassword(Id, "newPassword");
            Assert.IsTrue(adminRepos.GetAccount(Id).Password == "newPassword");
            Assert.IsTrue(adminRepos.FindByPassword("22", "name2").Email == "email2");

            //Добавить удалить аккаунт
            adminRepos.AddAccount(new Account() { Name = "alexey", Email = "abc@gmail.com", Password = "1111", Rights = "user" });
            Assert.IsNotNull(adminRepos.FindByPassword("1111", "alexey"));
            Id = adminRepos.FindByPassword("1111", "alexey").Id;
            adminRepos.RemoveAccount(Id);
            Assert.IsNull(adminRepos.FindByPassword("1111", "alexey"));
        }

    }
}
