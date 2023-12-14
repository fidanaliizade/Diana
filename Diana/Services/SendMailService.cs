using System.Net;
using System.Net.Mail;

namespace Diana.Services
{
    public static class SendMailService
    {
        public static void SendMail(string to, string name )
        {
            using var client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("email","kjndc");
            client.EnableSsl = true;
        }
    }
}
