using ADB.BL.Helpers;
using ADB.BL.Interfaces;
using ADB.BL.Models;
using ADB.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ADB.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        #region Fields
        private readonly IEmployeeRepo employee;
        private readonly ICityRepo city;
        private readonly IDistrictRepo district;
        private readonly IDepartmentRepo department;
        private readonly IMapper mapper;
        #endregion

        #region Constructors
        public EmployeeController(IEmployeeRepo employee, ICityRepo city, IDistrictRepo district, IDepartmentRepo department, IMapper mapper)
        {
            this.employee = employee;
            this.city = city;
            this.district = district;
            this.department = department;
            this.mapper = mapper;
        }
        #endregion

        #region Actions
        public IActionResult Index(string SearchValue)
        {
            if(SearchValue == null)
            {
                var data = employee.Get();
                var model = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(model);
            }
            else
            {
                var data = employee.SearchByName(SearchValue);
                var model = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(model);
            }
        }

        // We can make new action for search results
        //[HttpPost]
        //public IActionResult Search(string SearchValue)
        //{
        //    var data = employee.SearchByName(SearchValue);
        //    return View(data);
        //}

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CvName = FileUploader.UplodFile("docs/", model.Cv);
                    model.ImageName = FileUploader.UplodFile("images/", model.Image);
                    var data = mapper.Map<Employee>(model);
                    employee.Create(data);
                    return RedirectToAction("Index");
                }
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
                return View(model);
            }
        }

        public IActionResult Details(int id)
        {
            var data = employee.GetById(id);
            var model = mapper.Map<EmployeeVM>(data);
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = employee.GetById(id);
            var model = mapper.Map<EmployeeVM>(data);
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CvName = FileUploader.UplodFile("docs/", model.Cv);
                    model.ImageName = FileUploader.UplodFile("images/", model.Image);
                    var data = mapper.Map<Employee>(model);
                    employee.Update(data);
                    return RedirectToAction("Index");
                }
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = employee.GetById(id);
            var model = mapper.Map<EmployeeVM>(data);
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(EmployeeVM model)
        {
            try
            {
                FileUploader.DeleteFile("docs/", model.CvName);
                FileUploader.DeleteFile("images/", model.ImageName);
                var data = mapper.Map<Employee>(model);
                employee.Delete(data);
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
                return View(model);
            }
        }
        #endregion

        #region Ajax Requests
        // Any Ajax request will be with type [HttpPost] to hide the data
        [HttpPost]
        public JsonResult GetCityDataByCountryId(int countryId)
        {
            var data = city.Get(C => C.CountryId == countryId);
            return Json(data);
        }

        [HttpPost]
        public JsonResult GetDistrictDataByCityId(int cityId)
        {
            var data = district.Get(D => D.CityId == cityId);
            return Json(data);
        }
        #endregion
    }
}
