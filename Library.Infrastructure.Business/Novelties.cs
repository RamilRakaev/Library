using Library.Domain.Core;
using Library.Services.Interfaces;
using MimeKit;
using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Library.Infrastructure.Bll.Data;

namespace Library.Infrastructure.Services
{
    public class Novelties:INovelties
    {
        public async void SendMessage(string password, Account account)
        {
            await SendEmailAsync(account.Email, "Никому не показывайте!", "Новый пароль: " + password);
        }

        public async void SendMessageAboutBooks( Account account, Book[] books)
        {
            string aboutBooks = "";
            foreach (Book book in books)
            {
                aboutBooks += " \n" + book.Title + " (" + book.Author + "),";
            }
            aboutBooks = aboutBooks.Substring(0, aboutBooks.Length - 2) + ".";

            await SendEmailAsync(account.Email, "Внимание новинка!", "Теперь вам доступны новые книги:" + aboutBooks);
        }

        private async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Библиотека", "librarysite32@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("librarysite32@gmail.com", "Library#Site32!");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
