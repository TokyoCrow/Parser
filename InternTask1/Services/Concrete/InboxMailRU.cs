﻿using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using InternTask1.Properties;
using InternTask1.Services.Abstract;

namespace InternTask1.Services.Concrete
{
    public class InboxMailRU : ISendEmail
    {
        readonly string host = "smtp.mail.ru";
        readonly int port = 587;
        public async void Send(StringBuilder mailText)
        {
            using (SmtpClient smtp = new SmtpClient(host, port))
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(Settings.Default.EmailForSending);
                message.Bcc.Add(new MailAddress(Configuration.ExpEmail));
                message.Subject = "Programm Errors";
                message.Body = mailText.ToString();
                smtp.Credentials = new NetworkCredential(Settings.Default.EmailForSending, Settings.Default.PasswordEFS);
                smtp.EnableSsl = true;
                try 
                { 
                    await smtp.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
