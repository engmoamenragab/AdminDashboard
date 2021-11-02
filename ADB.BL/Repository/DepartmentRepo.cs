using ADB.BL.Interfaces;
using ADB.BL.Models;
using ADB.DAL.Database;
using ADB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Repository
{
    public class DepartmentRepo: IDepartmentRepo
    {
        // The next line of code return compile error because we change the place of connection string
        // and added a constructor in AdminDashboardDbContext with parameter
        //AdminDashboardContext adminDashboardDb = new AdminDashboardContext();

        // So we now use the dependency Injection to avoid this error
        private readonly AdminDashboardContext adminDashboardDb;

        public DepartmentRepo(AdminDashboardContext adminDashboardDb)
        {
            this.adminDashboardDb = adminDashboardDb;
        }

        // Without Auto Mapper
        //public IEnumerable<DepartmentVM> Get()
        //{
        //    var Data = GetDepartment();
        //    return Data;
        //}

        // With Auto Mapper
        public IEnumerable<Department> Get()
        {
            var Data = GetDepartment();
            return Data;
        }

        // Without Auto Mapper
        //public DepartmentVM GetById(int Id)
        //{
        //    var Data = adminDashboardDb.Department.Where(D => D.Id == Id)
        //        .Select(D => new DepartmentVM
        //        {
        //            Id = D.Id,
        //            Name = D.Name,
        //            Code = D.Code
        //        })
        //        .FirstOrDefault();
        //    return Data;
        //}

        // With Auto Mapper
        public Department GetById(int Id)
        {
            var Data = adminDashboardDb.Department.Where(D => D.Id == Id).FirstOrDefault();
            return Data;
        }

        // Without Auto Mapper
        //public void Create(DepartmentVM model)
        //{
        //    Department Department = new Department();
        //    Department.Name = model.Name;
        //    Department.Code = model.Code;
        //    adminDashboardDb.Department.Add(Department);
        //    adminDashboardDb.SaveChanges();
        //}

        // With Auto Mapper
        public void Create(Department model)
        {
            adminDashboardDb.Department.Add(model);
            adminDashboardDb.SaveChanges();
        }

        // Without Auto Mapper
        //public void Update(DepartmentVM model)
        //{
        //    var OldData = adminDashboardDb.Department.Find(model.Id);
        //    OldData.Name = model.Name;
        //    OldData.Code = model.Code;
        //    adminDashboardDb.SaveChanges();
        //}

        // With Auto Mapper
        public void Update(Department model)
        {
            adminDashboardDb.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            adminDashboardDb.SaveChanges();
        }

        // Without Auto Mapper
        //public void Delete(int Id)
        //{
        //    var OldData = adminDashboardDb.Department.Find(Id);
        //    adminDashboardDb.Department.Remove(OldData);
        //    adminDashboardDb.SaveChanges();
        //}

        // With Auto Mapper
        public void Delete(Department model)
        {
            adminDashboardDb.Department.Remove(model);
            adminDashboardDb.SaveChanges();
        }

        public IEnumerable<Department> SearchByName(string name)
        {
            var data = adminDashboardDb.Department.Where(D => D.Name.Contains(name));
            return data;
        }

        // ======================= Refactor ==========================

        // Without Mapper
        //private IEnumerable<DepartmentVM> GetDepartment()
        //{
        //    return adminDashboardDb.Department.Select(D => new DepartmentVM
        //    { 
        //        Id = D.Id,
        //        Name = D.Name,
        //        Code = D.Code 
        //    });
        //}

        // With Mapper
        private IEnumerable<Department> GetDepartment()
        {
            return adminDashboardDb.Department.Select(D => D);
        }
    }
}
