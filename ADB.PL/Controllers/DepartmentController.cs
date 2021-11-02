using ADB.BL.Interfaces;
using ADB.BL.Models;
using ADB.BL.Repository;
using ADB.DAL.Entities;
using ADB.PL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADB.PL.Controllers
{
    /// <summary>
    /// Department controller to hold all data belongs to Department (CRUD)
    /// </summary>
    public class DepartmentController : Controller
    {
        #region Fields
        // Lossely Coupled
        private readonly IDepartmentRepo department;
        private readonly IMapper mapper;

        // Tightly Coupled
        //DepartmentRepo department;

        #endregion

        #region Constructors
        public DepartmentController(IDepartmentRepo department, IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }
        #endregion

        #region Actions
        public IActionResult Index(string SearchValue)
        {
            #region Description
            //// All next variables are local that is mean I can send it's value to it's view only

            //// [ViewData] Like Object Type
            //ViewData["X"] = "Hi I am View Data";

            //// [ViewBag] Like Dynamic
            //ViewBag.Y = "Hi I am View Bag";

            //// [TempData] Like Object
            //TempData["Z"] = "Hi I am Temp Data";

            //string[] Names = { "Moamen", "Muhammad", "Ragab" };

            //ViewBag.Data = Names;

            //var Emp01 = new Employee() { Id = 1, Name = "Moamen", Salary = 50000 };
            //var Emp02 = new Employee() { Id = 2, Name = "Muhammad", Salary = 40000 };
            //var Emp03 = new Employee() { Id = 3, Name = "Ragab", Salary = 30000 };

            //var EmpsList = new List<Employee>();
            //EmpsList.Add(Emp01);
            //EmpsList.Add(Emp02);
            //EmpsList.Add(Emp03);

            //ViewBag.Data = EmpsList;

            //return RedirectToAction("Index", "Home"); 
            #endregion

            if (SearchValue == null)
            {
                var data = department.Get();
                var model = mapper.Map<IEnumerable<DepartmentVM>>(data);
                return View(model);
            }
            else
            {
                var data = department.SearchByName(SearchValue);
                var model = mapper.Map<IEnumerable<DepartmentVM>>(data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(model);
                    department.Create(data);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        public IActionResult Details(int id)
        {
            var data = department.GetById(id);
            var model = mapper.Map<DepartmentVM>(data);
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = department.GetById(id);
            var model = mapper.Map<DepartmentVM>(data);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(DepartmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(model);
                    department.Update(data);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = department.GetById(id);
            var model = mapper.Map<DepartmentVM>(data);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(DepartmentVM model)
        {
            try
            {
                var data = mapper.Map<Department>(model);
                department.Delete(data);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
        #endregion
    }
}
