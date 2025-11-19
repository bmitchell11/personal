using FinalProject.Data;
using FinalProject.Models;

using System.Net.Mail;
using System.Net;
using System.Text;
using System.IO;

namespace FinalProject.Services
{
    public class EmailSender
    {
        public void SendEmail(string senderEmail, string password, string toEmail, string subject)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(senderEmail);
                message.To.Add(toEmail);
                message.Subject = subject;

                string htmlContent = @"
                <html>
                    <body>
                        <p>Dear Resident,</p>
                        <p>We have received a package for you at the leasing office. Due to limited storage space, please pick up your package within <strong>5 days</strong>.</p>
                        <p>If the package is not picked up within this time frame, it will be returned to the post office.</p>
                        <p>Thank you!</p>
                        <p>BuffTeks Apartment Leasing Office</p>
                    </body>
                </html>
                ";

                message.AlternateViews.Add(
                    new AlternateView(
                        new MemoryStream(Encoding.UTF8.GetBytes(htmlContent)),
                        "text/html"
                    )
                );

                SmtpClient smtpClient = new SmtpClient("mail.privateemail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail, password)
                };

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }
}
