using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UptimeMonitor
{
    public static class Gmailer
    {
        public static void SendTextMessage(string body)
        {
            using (var client = new SmtpClient())
            {
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                
                client.Credentials = new NetworkCredential("bwalker465@gmail.com", "eh");
                using (
                    var emailMessage = new MailMessage(
                    from: new MailAddress("bwalker465@gmail.com", "Walkernet Alerts"),
                    to: new MailAddress("4073345849@tmomail.net", string.Empty)
                    ))
                {

                    emailMessage.Subject = "";
                    emailMessage.Body = body;
                    

                    client.Send(emailMessage);
                }
            }



        }
    }
}
