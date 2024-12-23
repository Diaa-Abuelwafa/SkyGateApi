using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SkyGateDomainLayer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateServiceLayer.Helpers
{
    public static class EmailHelper
    {
        public static void SendEmail(Email Mail, IConfiguration Config)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(Config["MailSettings:DisplayName"], Config["MailSettings:From"]));
            message.To.Add(MailboxAddress.Parse(Mail.To));
            message.Subject = Mail.Subject;

            var builder = new BodyBuilder();
            builder.TextBody = Mail.Body;
            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(Config["MailSettings:Host"], int.Parse(Config["MailSettings:Port"]));

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(Config["MailSettings:From"], Config["MailSettings:Password"]);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
