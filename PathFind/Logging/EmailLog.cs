using Logging.Interface;
using System;
using System.Net;
using System.Net.Mail;

namespace Logging
{
    public sealed class EmailLog : ILog
    {
        public EmailLog(MailAddress from, MailAddress to, SmtpClient client)
        {
            this.from = from;
            this.to = to;
            this.client = client;
            client.EnableSsl = true;
        }

        public EmailLog()
        {
            var creds = new NetworkCredential("pathfindingproject@gmail.com", "gUBDmOAh");
            client = new SmtpClient("smtp.gmail.com", 587);
            from = new MailAddress("pathfindingproject@gmail.com", "Pathfinding loggin system");
            to = new MailAddress("pathfindingproject@gmail.com");
            client.Credentials = creds;
        }

        public void Debug(string message)
        {
            
        }

        public void Error(Exception ex, string message = null)
        {
            SendMessage("Error", ex.Message + "\n" + ex.StackTrace.ToString() + "\n" + message);
        }

        public void Error(string message)
        {
            SendMessage("Error", message);
        }

        public void Fatal(Exception ex, string message = null)
        {
            SendMessage("Fatal", ex.Message + "\n" + ex.StackTrace.ToString() + "\n" + message);
        }

        public void Fatal(string message)
        {
            SendMessage("Fatal", message);
        }

        public void Info(string message)
        {
            
        }

        public void Trace(string message)
        {
            
        }

        public void Warn(Exception ex, string message = null)
        {
            SendMessage("Warn", ex.Message + "\n" + ex.StackTrace.ToString() + "\n" + message);
        }

        public void Warn(string message)
        {
            SendMessage("Warn", message);
        }

        private async void SendMessage(string level, string message)
        {
            var msg = new MailMessage(from, to);
            msg.Subject = $"Level: {level}";
            msg.Body = message;
            await client.SendMailAsync(msg);
        }

        private readonly MailAddress from;
        private readonly MailAddress to;
        private readonly SmtpClient client;
    }
}
