using ApiProyectoExtraSlice.Helpers;
using AWSLambdaCorreos.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace MVCApiExtraSlice.Helpers
{
    public class HelperMail
    {
        private ModelEmail model;
        public HelperMail(ModelEmail model)
        {
            this.model = model;
        }
        public async Task SendMailAsync(string para, string asunto, string mensaje)
        {
            MailMessage mail = this.ConfigureMailMessage(para, asunto, mensaje);
            SmtpClient client = this.ConfigureSmtpClient();
            await client.SendMailAsync(mail);
        }

        public async Task SendMailAsync(string para, string asunto, string mensaje, List<Stream> filesPath)
        {
            MailMessage mail = this.ConfigureMailMessage(para, asunto, mensaje);
            SmtpClient client = this.ConfigureSmtpClient();
            foreach (Stream path in filesPath)
            {
                Attachment attachment = new Attachment(path, new System.Net.Mime.ContentType("application/pdf"));
                mail.Attachments.Add(attachment);
            }
            await client.SendMailAsync(mail);
        }

        public async Task SendMailAsync(string para, string asunto, string mensaje, string filePath)
        {
            MailMessage mail = this.ConfigureMailMessage(para, asunto, mensaje);
            SmtpClient client = this.ConfigureSmtpClient();
            Attachment attachment = new Attachment(filePath);
            mail.Attachments.Add(attachment);
            await client.SendMailAsync(mail);
        }

        private MailMessage ConfigureMailMessage(string para, string asunto, string mensaje)
        {
            MailMessage mailMessage = new MailMessage();
            string email = this.model.User;
            mailMessage.From = new MailAddress(email);
            mailMessage.To.Add(new MailAddress(para));
            mailMessage.Subject = asunto;
            mailMessage.Body = mensaje;
            mailMessage.IsBodyHtml = true;
            return mailMessage;
        }

        private SmtpClient ConfigureSmtpClient()
        {
            string user = this.model.User;
            string password = this.model.Password;
            string host = this.model.Host;
            int port = 587;
            bool enableSSL = this.model.EnableSsl;
            bool defaultCredentials = this.model.DefaultCredentials;
            SmtpClient client = new SmtpClient();
            client.Host = host;
            client.Port = port;
            client.EnableSsl = enableSSL;
            client.UseDefaultCredentials = defaultCredentials;
            NetworkCredential credentials = new NetworkCredential(user, password);
            client.Credentials = credentials;
            return client;
        }
    }
}
