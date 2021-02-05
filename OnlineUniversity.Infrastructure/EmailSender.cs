using OnlineUniversity.Infrastructure.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnlineUniversity.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(Email email)
        {
            Console.WriteLine($"Sending e-mail to '{email.To}' ");
            return Task.CompletedTask;
        }
    }
}
