using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADB.PL.Controllers
{
    public class TestInjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
