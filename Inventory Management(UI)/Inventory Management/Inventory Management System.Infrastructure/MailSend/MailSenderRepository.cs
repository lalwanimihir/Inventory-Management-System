using Inventory_Management_System.Application.Services.IMailSendService;
using System.Net;
using System.Net.Mail;
using static Inventory_Management_System.Shared.Constants.CommonConstantsValues;

namespace Inventory_Management_System.Infrastructure.MailSend
{
    public class MailSenderRepository : IMailSender
    {
        public async Task SendMailToUser(IEnumerable<string?> emails)
        {
            string SenderMail = adminMail;
            string password = mailPassword;

            Parallel.ForEach(emails, email =>
            {
                if (!string.IsNullOrEmpty(email))
                {
                    MailMessage message = new()
                    {
                        From = new MailAddress(SenderMail),
                        Subject = "Promotion Available!",
                        IsBodyHtml = true,
                        Body = mailBody
                    };

                    message.To.Add(email);

                    var client = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(SenderMail, password),
                        EnableSsl = true
                    };
                     client.SendMailAsync(message);
                }
            });
        }
    }
}
