using Booking.Interfaces;
using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Booking.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSendConfiguration _smtpSendConfiguration;
        public EmailSender()
        {
            _smtpSendConfiguration = new SmtpSendConfiguration();
        }
        public  Result SendEmail(EmailStringAttachment message)
        {
            MailMessage mailObj = (MailMessage)
                BuildMessage(message);


            if (message.AttachmentsFilePath != null &&
                message.AttachmentsFilePath.Count > 0)
            {
                foreach (string pathToFile in message.AttachmentsFilePath)
                {
                    mailObj.Attachments.Add(new Attachment(pathToFile, MediaTypeNames.Application.Octet));
                }
            }

            return this.SendMessageAsync(mailObj);
        }
        private  Result SendMessageAsync(MailMessage message)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential
                    (this._smtpSendConfiguration.DefaultEmailFrom,
                        this._smtpSendConfiguration.Password)
                })
                {
                    smtpClient.Send(message);

                    return new Result()
                    {
                        HttpStatusCode = HttpStatusCode.OK
                    };
                }
            }
            catch
            {
                return new Result()
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError
                };
            };
        }


        private MailMessage BuildMessage(EmailMessage message)
        {
            MailAddress from = new MailAddress(_smtpSendConfiguration.DefaultEmailFrom,
                _smtpSendConfiguration.DefaultNameFrom);
            MailMessage mailObj =
                new MailMessage(_smtpSendConfiguration.DefaultEmailFrom,
                    message.EmailTo,
                    message.Subject,
                    message.Message);

            mailObj.Sender = from;
            mailObj.BodyEncoding = Encoding.UTF8;
            mailObj.SubjectEncoding = Encoding.UTF8;
            mailObj.From = from;

            return mailObj;
        }
    }
}