using ADB.BL.Interfaces;
using ADB.DAL.Database;
using ADB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Repository
{
    public class CityRepo : ICityRepo
    {
        private readonly AdminDashboardContext adminDashboardDb;

        public CityRepo(AdminDashboardContext adminDashboardDb)
        {
            this.adminDashboardDb = adminDashboardDb;
        }

        public IEnumerable<City> Get(Expression<Func<City, bool>> filter = null)
        {
            if(filter == null)
            {
                var data = adminDashboardDb.City.Select(C => C);
                return data;
            }
            else
            {
                return adminDashboardDb.City.Where(filter);
            }
        }
    }
}
