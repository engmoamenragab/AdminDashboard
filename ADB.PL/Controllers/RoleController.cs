using ADB.BL.Models;
using ADB.DAL.Extends;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADB.PL.Controllers
{
    public class RoleController : Controller
    {
        #region Fields
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        #endregion

        #region Constructors
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        #endregion

        #region Get
        public IActionResult Index()
        {
            var Roles = roleManager.Roles;
            return View(Roles);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var role = new IdentityRole()
                    {
                        Name = model.Name,
                        NormalizedName = model.Name.ToUpper()
                    };
                    var result = await roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                    return View(model);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Update(IdentityRole model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var role = await roleManager.FindByIdAsync(model.Id);
                    role.Name = model.Name;
                    role.NormalizedName = model.Name.ToUpper();
                    var result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                    return View(model);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        #endregion

        #region AddOrRemoveUsers
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            ViewBag.RoleId = RoleId;
            var role = await roleManager.FindByIdAsync(RoleId);
            var model = new List<UserInRoleVM>();
            foreach (var user in userManager.Users)
            {
                var userInRole = new UserInRoleVM()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }
                model.Add(userInRole);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVM> model, string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (i < model.Count)
                    continue;
            }
            return RedirectToAction("Update", new { id = RoleId });
        }
        #endregion
    }
}
