using AutoMapper.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using VetHub02.Core.Entities;

namespace VetHub02.Helpers
{
    public class MailSettingsimplementation : IMailSettings
    {
        private readonly MailSettings options;
        public MailSettingsimplementation(IOptions<MailSettings> options)
        {

            this.options = options.Value;
        }
        public void sendMail(MailSettingsModel email)
        {
            var mail = new MimeMessage {Sender = MailboxAddress.Parse(options.Email) ,
                                        Subject = email.Subject};
            mail.To.Add(MailboxAddress.Parse(email.To));

            mail.From.Add(new MailboxAddress(options.Email, options.DisplayName));

           var builder  = new BodyBuilder();
            builder.TextBody = email.Body;
       
            mail.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(options.Host, options.Port ,SecureSocketOptions.StartTls);
            smtp.Authenticate(options.Email, options.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);

        }
        //public async Task SendPasswordResetEmail(string email, string resetLink)
        //{

        //    try
        //    {
        //        var mail = new MimeMessage();
        //        mail.Sender = MailboxAddress.Parse(options.Email);
        //        mail.Subject = "Password Reset";
        //        mail.To.Add(MailboxAddress.Parse(email));
        //        mail.From.Add(new MailboxAddress(options.DisplayName, options.Email));

        //        var builder = new BodyBuilder();
        //        builder.TextBody = $"Click the following link to reset your password: {resetLink}";
        //        mail.Body = builder.ToMessageBody();

        //        using var smtp = new SmtpClient();
        //        await smtp.ConnectAsync(options.Host, options.Port, SecureSocketOptions.StartTls);
        //        await smtp.AuthenticateAsync(options.Email, options.Password);
        //        await smtp.SendAsync(mail);
        //        await smtp.DisconnectAsync(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions here (log, return error response, etc.)
        //        Console.WriteLine($"Failed to send email: {ex.Message}");
        //        throw; // Optionally, rethrow the exception
        //    }
        //    //await SendEmail(email, "Password Reset", $"Click <a href=\"{resetLink}\">here</a> to reset your password.");

        //}

    }

        //private Task SendEmailAsync(string email, string subject, string message)
        //{
        //    // Implement your email sending logic here
        //    throw new NotImplementedException();
        //}

    }
