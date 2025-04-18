using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Pexel.Application.Contracts.Services;
using Pexel.Core.Common;


namespace Pexel.Infrastructrue.Implementation
{
    public class SendEmail : ISendEmail
    {
        private readonly MailSetting _mailSetting;

        public SendEmail(IOptions<MailSetting> mailSetting)
        {
            _mailSetting = mailSetting.Value;
        }

        public void SendMail(string mailTo, string Subject, string Message)
        {
            using (var Client = new SmtpClient())
            {
                Client.Connect(_mailSetting.Host, _mailSetting.Port);
                Client.Authenticate(_mailSetting.Email, _mailSetting.Password);


                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = Message,
                    TextBody = "Hello",
                };

                var message = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody()
                };

                message.From.Add(new MailboxAddress("Pexel Store", _mailSetting.Email));
                message.To.Add(new MailboxAddress("Mr", mailTo));
                message.Subject = Subject;
                Client.Send(message);
                Client.Disconnect(true);
            }
        }
    }
}
