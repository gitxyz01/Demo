using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{    
     public class EmailHelper
        {
            public void Send(
                string subject,
                string receiver,
                string body,
                IList<string> cc,
                IList<string> bcc
                )
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Credentials = new NetworkCredential("skypexyz01@gmail.com", "rglzefqqpxvhldmo");
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("skypexyz01@gmail.com");
                    message.To.Add(receiver);

                    if (cc != null)
                    {
                        foreach (var c in cc)
                        {
                            message.CC.Add(c);
                        }
                    }
                    if (bcc != null)
                    {
                        foreach (var b in bcc)
                        {
                            message.Bcc.Add(b);
                        }
                    }
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    smtpClient.Send(message);

                }
            }
        }
}
