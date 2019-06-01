/*
 * Пространство имен System содержит фундаментальные и базовые классы, 
 * определяющие часто используемые типы значений и ссылочных данных, события и обработчики событий, 
 * интерфейсы, атрибуты и исключения обработки.
 */
using System;
/*
 * Пространство имен System.Net содержит сетевые классы для поиска IP-адресов, сетевой аутентификации, 
 * разрешений, отправки и получения данных.
 */
using System.Net;
/*
 * Пространство имен System.Net.Mail содержит классы, используемые для отправки электронной почты 
 * на сервер SMTP (Simple Mail Transfer Protocol) для доставки.
 */
using System.Net.Mail;

namespace Backup
{
    // Класс "Рассылка"
    class Mailer
    {
        public static void SendMail(
            string smtpServer, 
            string from, 
            string password,
            string mailto, 
            string caption, 
            string message, 
            string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;

                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));

                SmtpClient client = new SmtpClient();

                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);

                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }
    }
}
