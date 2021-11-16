using ADB.BL.Models;
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
        [HttpPost]
        public IActionResult Signup(SignupVM model)
        {
            try
            {
                if(ModelState.IsValid)
                {

                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
        #endregion

        #region Signin
        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signin(SigninVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
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
        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
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
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }
        #endregion
    }
}
