using Library.Infrastructure.Bll.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            LibrarianRepos librarianRepos = new LibrarianRepos(new LibraryContext(new DbContextOptionsBuilder
                <LibraryContext>().UseSqlServer
                (@"Server=(localdb)\mssqllocaldb;Database=
                testdb;Trusted_Connection=True;").Options));
            librarianRepos.AddBook(new Library.Infrastructure.Bll.Data.Book() {Title="@d" });
            Console.WriteLine("Hello World!");
        }
    }
}
