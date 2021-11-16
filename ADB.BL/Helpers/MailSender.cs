using ADB.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Helpers
{
    public static class MailSender
    {
        public static string SendMail(MailVM model)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("eng.moamen.ragab@gmail.com", "");
                smtp.Send("eng.moamen.ragab@gmail.com", model.Email, model.Title, model.Message);
                var result = "Done Mail Sent!";
                return result;
            }
            catch (Exception ex)
            {
                var result = "Failed To Sent!";
                return result;
            }
        }
    }
}
