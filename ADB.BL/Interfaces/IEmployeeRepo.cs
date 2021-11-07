using ADB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Interfaces
{
    public interface IEmployeeRepo
    {
        IEnumerable<Employee> Get();
        Employee GetById(int Id);
        Employee Create(Employee model);
        Employee Update(Employee model);
        void Delete(Employee model);
        IEnumerable<Employee> SearchByName(string name);
    }
}
