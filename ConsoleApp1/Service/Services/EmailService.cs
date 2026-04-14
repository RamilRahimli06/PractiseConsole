using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmailService
    {
        public void SendVerificationCode(string toEmail, string code)
        {
            var fromEmail = "ramiler-apa104@code.edu.az";
            var password = "edmx ncix qjgo ypye"; 

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = "Verification Code",
                Body = $"Your OTP code is: {code}",
                IsBodyHtml = false
            };

            message.To.Add(toEmail);

            smtp.Send(message);

        }
    }
}
