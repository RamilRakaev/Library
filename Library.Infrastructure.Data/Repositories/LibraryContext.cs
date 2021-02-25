using Library.Infrastructure.Bll.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Infrastructure.Bll.Repositories
{
    public class LibraryContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book[]
                {
                    new Book()
                    {
                        Id=1,
                        Title="Алгоритмы. Руководство по разработке",
                        ImagePath="1.jpg",
                        IsBusy=false,IsBorrow=false, BookRequests=0,State="Идеальное",
                        Author="Стивен Скиена",Genre="Программирование",
                        Publisher="Springer",PublisherYear=2019,
                        Description="Книга является наиболее полным руководством по " +
                        "разработке эффективных алгоритмов. Первая часть книги содержит " +
                        "практические рекомендации по разработке алгоритмов: приводятся " +
                        "основные понятия, дается анализ алгоритмов, рассматриваются типы " +
                        "структур данных, основные алгоритмы сортировки, операции обхода " +
                        "графов и алгоритмы для работы со взвешенными графами, примеры " +
                        "использования комбинаторного поиска, эвристических методов и " +
                        "динамического программирования. Вторая часть книги содержит " +
                        "обширный список литературы и каталог из 75 наиболее " +
                        "распространенных алгоритмических задач, для которых перечислены " +
                        "существующие программные реализации. Приведены многочисленные " +
                        "примеры задач."
                    },
                    new Book()
                    {
                        Id=2,
                        Title="Введение в структуры и алгоритмы обработки данных",
                        ImagePath="2.jpg",
                        IsBusy=false,IsBorrow=false, BookRequests=0,State="Идеальное",
                        Author="Курносов Михаил Георгиевич",Genre="Программирование",
                        Publisher="Сибирский государственный университет телекоммуникаций и информатики (Новосибирск)",
                        PublisherYear=2016,Description="Часть теоретического материала по курсу \"Структуры и алгоритмы данных\"," +
                        "который автор читал студентам 1 и 2 годов обучения в СГУТИ. При доказательстве оценок вычислительной" +
                        "сложности алгоритмов сведено к минимуму использование аппарата теории вероятностей, а там, где он " +
                        "применён, даны комментарии"
                    },
                    new Book()
                    {
                        Id=3,
                        Title="Краткий курс логики. Искусство правильного мышления",
                        ImagePath="3.jpg",
                        IsBusy=false,IsBorrow=false, BookRequests=0,State="Идеальное",
                        Author="Дмитрий Гусев",Genre="Логика",
                        Publisher="НЦ ЭНАС",PublisherYear=2003, Description="Что такое логика? Чем занимается эта древняя и в то же " +
                        "время всегда молодая наука? Зачем она нужна, можно ли без нее обойтись, и какую роль она играет в жизни " +
                        "человека? Что такое формы мышления и каковы основные законы мышления? К чему приводят многочисленные логические " +
                        "ошибки, которые мы непроизвольно или сознательно допускаем в мышлении и речи? Что такое доказательство и каковы " +
                        "его разновидности? Что представляют собой основные правила доказательства и ошибки, возникающие при их нарушении? " +
                        "Как сделать свои мысли ясными и отчетливыми, как надо их выражать, чтобы окружающие всегда понимали, что вы хотите " +
                        "сказать; как отстаивать свою точку зрения и убеждать собеседника? Как грамотно вести дискуссию и одерживать победу в " +
                        "споре? Что такое софизмы и логические парадоксы? Обо всем этом вы узнаете, прочитав книгу, которая отличается от " +
                        "многих других учебных пособий по логике тем, что читать ее будет нетрудно: автор, много лет преподающий логику " +
                        "студентам и школьникам, постарался сделать предлагаемый вашему вниманию материал простым и ясным, а по возможности " +
                        "- интересным и увлекательным.Книга адресована студентам и школьникам, изучающим логику, преподавателям - в качестве " +
                        "обмена педагогическим опытом - и всем, интересующимся логикой и другими гуманитарными науками."

                    },
                    new Book()
                    {
                        Id=4,
                        Title="Язык программирования C#",
                        ImagePath="4.jpg",
                        IsBusy=false,IsBorrow=false, BookRequests=0,State="Идеальное",
                        Author="Хейлсберг Андерс, Торгерсен Мэдс, Вилтамут Скотт, Голд Питер",Genre="Программирование",
                        Publisher="Питер",PublisherYear=2012, Description="Перед вами - четвёртое издание главной" +
                        "книги по языку С#, написанной легендой программирования - Андерсом Хейлсбергом, архитектором" +
                        "C#, Delphi и Turbo Pascal, совместно с другими специалистами, входившими в группу разработчиков" +
                        "C# компании Microsoft"
                    }
                }
                );
            modelBuilder.Entity<Account>().HasData(
                    new Account[]
                    {
                        new Account()
                        {
                            Id=1,
                            Name="Виктор Степанов",
                            Email="stepa@gmail.com",
                            Password="123",
                            Rights="admin"
                        },
                        new Account()
                        {
                            Id=2,
                            Name="Иван Гамазов",
                            Email="gamaz@gmail.com",
                            Password="123",
                            Rights="librarian"
                        },
                        new Account()
                        {
                            Id=3,
                            Name="Наталья Ахметова",
                            Email="chico5listo4@gmail.com",
                            Password="123",
                            Rights="user"
                        },
                        new Account()
                        {
                            Id=4,
                            Name="Дмитрий Алексеев",
                            Email="chico5listo4@gmail.com",
                            Password="3fd43rt$35g:",
                            Rights="user"
                        }
                    }
                );

            modelBuilder.Entity<Book>().Property(b => b.ImagePath).HasDefaultValue("img.png");
            modelBuilder.Entity<Book>().Property(b => b.Description).HasDefaultValue("Здесь должно быть описание книги");
            modelBuilder.Entity<Book>().Property(b => b.Author).HasDefaultValue("Автор книги");
            modelBuilder.Entity<Book>().Property(b => b.Title).HasDefaultValue("Название книги");
        }
    }
}
