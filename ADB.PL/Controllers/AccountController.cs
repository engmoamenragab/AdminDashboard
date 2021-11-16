using ADB.BL.Helpers;
using ADB.BL.Models;
using ADB.DAL.Extends;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADB.PL.Controllers
{
    public class AccountController : Controller
    {
        #region Fields
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        #endregion

        #region Constructors
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        } 
        #endregion

        #region Signup
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(SignupVM model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var user = new ApplicationUser()
                    {
                        Email = model.Email,
                        UserName = model.Email,
                        IsAgree = model.IsAgree
                    };
                    // We pass the password separate to make it hash
                    var result = await userManager.CreateAsync(user, model.Password);

                    if(result.Succeeded)
                    {
                        return RedirectToAction("Signin");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
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
        public async Task<IActionResult> Signin(SigninVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid UserName Or Password");

                    }
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
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Signin");
        }
        #endregion

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Get the user whos request the password reset
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        // Generate token
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);

                        // Generate the reset link
                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                        // Send the email
                        MailSender.SendMail(new MailVM() { Title = "Reset Password - Admin Dashboard", Message = passwordResetLink, Email = model.Email });

                        return RedirectToAction("ConfirmForgetPassword");
                    }

                    return RedirectToAction("ConfirmForgetPassword");
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
        public IActionResult ResetPassword(string email, string token)
        {
            if(email != "" && token != "")
            {
                return View();
            }
            return RedirectToAction("Signin");
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("ConfirmResetPassword");
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }

                    return RedirectToAction("ConfirmResetPassword");
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
