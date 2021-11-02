using ADB.BL.Models;
using ADB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Interfaces
{
    public interface IDepartmentRepo
    {
        // Without Auto Mapper
        //IEnumerable<DepartmentVM> Get();
        //DepartmentVM GetById(int Id);
        //void Create(DepartmentVM Obj);
        //void Update(DepartmentVM Obj);
        //void Delete(int Id);

        // With Auto Mapper
        IEnumerable<Department> Get();
        Department GetById(int Id);
        void Create(Department model);
        void Update(Department model);
        void Delete(Department model);
        IEnumerable<Department> SearchByName(string name);
    }
}
