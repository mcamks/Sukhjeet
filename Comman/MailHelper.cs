using System;
using System.Net;
using System.Net.Mail;

namespace BluSignalHelpMethod
{
    public class MailHelper
    {
        /// <summary>
        /// Sends an mail message
        /// </summary>
        /// <param name="to">Recepient address (comma separated for multiple addresses)</param>
        /// <param name="bcc">Bcc recepient (comma separated for multiple addresses)</param>
        /// <param name="cc">Cc recepient (comma separated for multiple addresses)</param>
        /// <param name="subject">Subject of mail message</param>
        /// <param name="body">Body of mail message</param>
        /// <param name="isBodyHtml"></param>
        public static void SendMailMessage(string to, string bcc, string cc, string subject, string body, bool isBodyHtml, string EmailFrom = "", string EmailID = "")
        {
            try
            {
                string EID = EmailID == "" ? CommonConfig.EMailId : EmailID;
                string EName = EmailFrom == "" ? CommonConfig.EMailName : EmailFrom;

                var mail = new MailMessage { From = new MailAddress(EID, EName) };
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = isBodyHtml;
                var smtp = new SmtpClient(CommonConfig.SmtpHostName, CommonConfig.PortNumber)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = CommonConfig.EnableSsl,
                    Credentials =
                        new NetworkCredential(CommonConfig.SmtpUserName,
                                              CommonConfig.SmtpPassword)
                };
                smtp.Send(mail);
            }
            catch (Exception ex)
            {

                string expMessage = ex.Message;
            }
        }

        public static void SendMailMessageAsync(string to, string bcc, string cc, string subject, string body)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(CommonConfig.EMailId, CommonConfig.EMailName)
            };
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            var smtp = new SmtpClient(CommonConfig.SmtpHostName, CommonConfig.PortNumber)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(CommonConfig.SmtpUserName, CommonConfig.SmtpPassword)
            };
            object state = mail;
            smtp.SendAsync(mail, state);
        }

    }
}
