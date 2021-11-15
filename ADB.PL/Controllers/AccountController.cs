using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADB.PL.Controllers
{
    public class AccountController : Controller
    {
        #region Signup
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        #endregion

        #region Signin
        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }
        #endregion

        #region Signout
        [HttpGet]
        public IActionResult Signout()
        {
            return View();
        }
        #endregion

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }
        #endregion
    }
}
