using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WePhone.Models;

namespace WePhone.Services
{

    public class EmailService : IEmailService
    {
        private readonly SMTP_Model _smtpConfig;
        private const string temppath = @"EmailTemplate/{0}.html";

        public async Task TestMail(UserEmailOption userEmailOption)
        {
            userEmailOption.Subject = "We been trying to reach you about your car extended warrenty";
            userEmailOption.Body = UpdatePlaceHolder(GetMailBody("ForgetPass"), userEmailOption.PlaceHolder);

            await SendEmail(userEmailOption);
        }

        public EmailService(IOptions<SMTP_Model> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }

        private async Task SendEmail(UserEmailOption userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach (var toEmail in userEmailOptions.ToMails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }

        private string GetMailBody(string temp)
        {
            var body = File.ReadAllText(string.Format(temppath, temp));
            return body;
        }

        private string UpdatePlaceHolder(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuePairs!=null) 
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if(text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }
            return text;
        }


        //public async Task SendEmailForForgotPassword(UserEmailOption userEmailOptions)
        //{
        //    userEmailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}}, reset your password.", userEmailOptions.PlaceHolders);

        //    userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ForgotPassword"), userEmailOptions.PlaceHolders);

        //    await SendEmail(userEmailOptions);
        //}
    }


}
