using Library.Domain.Interfaces.IData;
using Library.Infrastructure.Bll.Data;
using Library.Infrastructure.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Library.Infrastructure.Bll.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Infrastructure.Tests
{
    [TestClass()]
    public class RepositoryTest
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        DbContextOptions<LibraryContext> connectionOption = new DbContextOptionsBuilder
                <LibraryContext>().UseSqlServer
                (@"Server=(localdb)\mssqllocaldb;Database=
                testdb;Trusted_Connection=True;").Options;

        /// <summary>
        /// Подготовка начальных данных для бд
        /// </summary>
        void Start()
        {
            using (LibraryContext db = new LibraryContext(connectionOption))
            {
                db.Accounts.AddRange(
                    new Account() { Name = "name1",Password="11", Email="email1", Rights = "user" },
                    new Account() { Name = "name2",Password="22", Email="email2", Rights = "user" },
                    new Account() { Name = "name3", Password = "33", Email = "email3", Rights = "admin" });
                db.Books.AddRange(
                    new Book() { Title = "title1", Author = "author1", Publisher = "publisher1", Genre = "genre1",IsBusy=true,IsBorrow=true },
                    new Book() { Title = "title2", Author = "author2", Publisher = "publisher2", Genre = "genre2" });
                db.Histories.AddRange(
                    new History(2, 1, 2) { StartDate = new DateTime(2021, 1, 1), EndDate = new DateTime(2021, 1, 3), BookingTimeLib = 1 });
                db.SaveChanges();

            }
        }

        /// <summary>
        /// Удаление данных из бд
        /// </summary>
        [TestMethod()]
        public void End()
        {
            using (LibraryContext db = new LibraryContext(connectionOption))
            {
                while (db.Accounts.Count() > 0)
                {
                    db.Accounts.Remove(db.Accounts.First());
                    db.SaveChanges();
                }
                while (db.Books.Count() > 0)
                {
                    db.Books.Remove(db.Books.First());
                    db.SaveChanges();
                }
                while(db.Histories.Count() > 0)
                {
                    db.Histories.Remove(db.Histories.First());
                    db.SaveChanges();
                }
                while (db.Comments.Count() > 0)
                {
                    db.Comments.Remove(db.Comments.First());
                    db.SaveChanges();
                }
            }
        }

        [TestMethod(),TestCategory("Integration")]
        public void UserReposTest()
        {
            Start();
            //Иницилизация
            UserRepos userRepos = new UserRepos(new LibraryContext(connectionOption));
            Assert.IsNotNull(userRepos);

            //Бронировка
            int Id = userRepos.AllBooks.ElementAt(1).Id;
            userRepos.BusyBook(Id, 1);
            Assert.IsTrue(userRepos.GetBook(Id).IsBusy);
            Assert.AreEqual(userRepos.GetBook(Id).IdAccount, 1);

            userRepos.TakeOff(Id);
            Assert.IsFalse(userRepos.GetBook(Id).IsBusy);

            //Поиск книг
            Assert.AreEqual(userRepos.FindBookByAuthor("author1").First().Title, "title1");
            Assert.AreEqual(userRepos.FindBookByPublisher("publisher2").First().Title, "title2");
            Assert.AreEqual(userRepos.FindBookByGenre("genre1").First().Title, "title1");
            Assert.IsNotNull(userRepos.FindBookByGenre("12"));

            //Комментарии
            Comment comment1 = new Comment() { IdBook = 1, TextComment = "comment1" };
            Comment comment2 = new Comment() { IdBook = 1, TextComment = "comment2" };
            userRepos.MakeComment(comment1);
            userRepos.MakeComment(comment2);
            Assert.AreEqual(userRepos.ReadComments(1).ElementAt(0).TextComment, "comment1");
            Assert.IsNotNull(userRepos.ReadComments(20));

            //Книги и аккаунты
            Assert.AreEqual(userRepos.AllBooks.Count(), 2);

            Assert.IsNotNull(userRepos.GetBook(10000));
            Assert.AreEqual(userRepos.GetBook(Id).Title, "title2");

            Assert.AreEqual(userRepos.MyAccount("11", "name1").Email, "email1");
            Assert.IsNotNull(userRepos.MyAccount("", ""));


            End();
        }

        /// <summary>
        /// Тест репозитория LibrarianRepos
        /// </summary>
        [TestMethod()]
        public void LibrarianReposTest()
        {
            Start();
            LibrarianRepos librarianRepos = new LibrarianRepos(new LibraryContext(connectionOption));

            //Информация о книгах
            int Id = librarianRepos.AllBooks.ElementAt(1).Id;
            Assert.IsTrue(librarianRepos.BusyBooks.ElementAt(0).IsBusy);
            Assert.IsFalse(librarianRepos.FreeBooks.ElementAt(0).IsBusy);
            Assert.IsTrue(librarianRepos.GivenBooks.ElementAt(0).IsBorrow);
            Assert.AreEqual(librarianRepos.GetBook(Id).Title, "title2");

            //Удалить, добавить
            librarianRepos.AddBook(new Book() { Title = "12345", Author = "author3", Publisher = "publisher3", Genre = "genre3", IsBusy = true, IsBorrow = true });
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
            Assert.IsTrue(librarianRepos.GetAccount(accountId).Penalty==200);

            Assert.IsTrue(librarianRepos.DeadlineOverdue(2, 1) == 1);

            librarianRepos.PayOffFine(accountId, 100);
            Assert.IsTrue(librarianRepos.GetAccount(accountId).Penalty == 100);
            End();
        }

        /// <summary>
        /// Тест репозитория AdminRepos
        /// </summary>
        [TestMethod()]
        public void AdminReposTest()
        {
            Start();

            AdminRepos adminRepos = new AdminRepos(new LibraryContext(connectionOption));

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
            adminRepos.AddAccount(new Account() { Name = "alexey", Email = "abc@gmail.com", Password = "1111",Rights="user" });
            Assert.IsNotNull(adminRepos.FindByPassword("1111", "alexey"));
            Id = adminRepos.FindByPassword("1111", "alexey").Id;
            adminRepos.RemoveAccount(Id);
            Assert.IsNull(adminRepos.FindByPassword("1111", "alexey"));
            End();
        }
    }
}
