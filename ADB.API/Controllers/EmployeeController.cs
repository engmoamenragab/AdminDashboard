using ADB.BL.Helpers;
using ADB.BL.Interfaces;
using ADB.BL.Models;
using ADB.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADB.API.Controllers
{
    //[Route("[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region For Test
        //[HttpGet]
        //[Route("~/api/GetData")] // This will override the default controller route
        //public string[] GetData()
        //{
        //    string[] arr = { "Moamen", "Muhammad", "Ragab" };
        //    return arr;
        //} 
        #endregion

        #region Fields
        private readonly IEmployeeRepo employee;
        private readonly IMapper mapper;
        #endregion

        #region Constructors
        public EmployeeController(IEmployeeRepo employee, IMapper mapper)
        {
            this.employee = employee;
            this.mapper = mapper;
        }
        #endregion

        #region APIs
        [HttpGet]
        [Route("~/api/GetEmployees")]
        public IActionResult GetEmployees()
        {
            try
            {
                var data = employee.Get();
                var model = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>()
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Retrieved",
                    Data = model
                });
            }
            catch(Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "No Data Found",
                    Error = ex.Message
                });
            }
        }

        // To override enable CORS in startup
        //[DisableCors]
        //[EnableCors("Server Name")]
        [HttpGet]
        [Route("~/api/GetEmployeeById/{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            try
            {
                var data = employee.GetById(id);
                var model = mapper.Map<EmployeeVM>(data);
                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Retrieved",
                    Data = model
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "No Data Found",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("~/api/CreateEmployee")]
        public IActionResult CreateEmployee(EmployeeVM model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(model);
                    var result = employee.Create(data);
                    return Ok(new ApiResponse<Employee>()
                    {
                        Code = "201",
                        Status = "Created",
                        Message = "Employee Created",
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<string>()
                    {
                        Code = "402",
                        Status = "Validation Error",
                        Message = "No Employee Created",
                        Error = "Invalid Data"
                    });
                }
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = "402",
                    Status = "Bad Request",
                    Message = "No Employee Created",
                    Error = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("~/api/UpdateEmployee")]
        public IActionResult UpdateEmployee(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(model);
                    var result = employee.Update(data);
                    return Ok(new ApiResponse<Employee>()
                    {
                        Code = "200",
                        Status = "Ok",
                        Message = "Employee Updated",
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<string>()
                    {
                        Code = "402",
                        Status = "Validation Error",
                        Message = "No Employee Updated",
                        Error = "Invalid Data"
                    });
                }
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = "402",
                    Status = "Bad Request",
                    Message = "No Employee Updated",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("~/api/DeleteEmployee")]
        public IActionResult DeleteEmployee(EmployeeVM model)
        {
            try
            {
                var data = mapper.Map<Employee>(model);
                employee.Delete(data);
                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Employee Deleted",
                    Data = model
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = "402",
                    Status = "Bad Request",
                    Message = "No Employee Deleted",
                    Error = ex.Message
                });
            }
        }
        #endregion
    }
}
