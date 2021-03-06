using ADB.BL.Interfaces;
using ADB.DAL.Database;
using ADB.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Repository
{
    public class EmployeeRepo: IEmployeeRepo
    {
        private readonly AdminDashboardContext adminDashboardDb;

        public EmployeeRepo(AdminDashboardContext adminDashboardDb)
        {
            this.adminDashboardDb = adminDashboardDb;
        }

        public IEnumerable<Employee> Get()
        {
            var Data = GetEmployee();
            return Data;
        }

        public Employee GetById(int Id)
        {
            var Data = adminDashboardDb.Employee.Include("Department").Where(D => D.Id == Id).FirstOrDefault();
            return Data;
        }

        public Employee Create(Employee model)
        {
            adminDashboardDb.Employee.Add(model);
            adminDashboardDb.SaveChanges();
            return adminDashboardDb.Employee.OrderBy(E => E.Id).LastOrDefault();
        }

        public Employee Update(Employee model)
        {
            adminDashboardDb.Entry(model).State = EntityState.Modified;
            adminDashboardDb.SaveChanges();
            return adminDashboardDb.Employee.Where(E => E.Id == model.Id).FirstOrDefault();
        }

        public void Delete(Employee model)
        {
            adminDashboardDb.Employee.Remove(model);
            adminDashboardDb.SaveChanges();
        }

        public IEnumerable<Employee> SearchByName(string name)
        {
            var data = adminDashboardDb.Employee.Include("Department").Where(E => E.Name.Contains(name));
            return data;
        }

        // ======================= Refactor ==========================
        private IEnumerable<Employee> GetEmployee()
        {
            return adminDashboardDb.Employee.Include("Department").Include("District").Select(E => E);
        }
    }
}
