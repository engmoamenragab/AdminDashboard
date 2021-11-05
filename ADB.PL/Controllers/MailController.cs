using ADB.BL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using ADB.BL.Helpers;

namespace ADB.PL.Controllers
{
    public class MailController : Controller
    {
        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(MailVM model)
        {
            TempData["Msg"] = MailSender.SendMail(model);
            ModelState.Clear();
            return View();
        }
    }
}
