using E_Shop.Business.Classes;
using E_Shop.Business.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Net.Imap;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Services
{
    public class EmailSender : IEmailSender
    {
        private EmailConfiguration emailConfiguration;

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            this.emailConfiguration = emailConfiguration;
        }

        public void SendEmail(string recipient, string subject, string emailBody)
        {
            string smtpServer = emailConfiguration.SmtpServer;
            int smtpPort = emailConfiguration.SmtpPort;
            string smtpUsername = emailConfiguration.SmtpUsername;
            string smtpPassword = emailConfiguration.SmtpPassword;
            string smtpEncryptionMethod = emailConfiguration.SmtpEncryptionMethod;

            var message = new MimeMessage();           
            message.To.Add(new MailboxAddress(recipient));
            message.From.Add(new MailboxAddress(smtpUsername));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailBody
            };

            using (var smtpClient = new SmtpClient())
            {
                // third parameter says that we want to use SSL
                smtpClient.Connect(smtpServer, smtpPort);
                // remove oauth functionality since we don't need it
                smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                smtpClient.Authenticate(smtpUsername, smtpPassword);
                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }
        }

        public void ReceiveEmail(string subject, string emailBody, string emailSender = null)
        {
            string ImapServer = emailConfiguration.ImapServer;
            int ImapPort = emailConfiguration.ImapPort;
            string ImapUsername = emailConfiguration.ImapUsername;
            string ImapPassword = emailConfiguration.ImapPassword;

            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(ImapUsername));
            message.From.Add(new MailboxAddress(emailSender));
            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailBody
            };

            using (var client = new ImapClient())
            {
                client.Connect(ImapServer, ImapPort, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(ImapUsername, ImapPassword);
                client.Inbox.Append(message);
                client.Disconnect(true);
            }




        }
    }
}
