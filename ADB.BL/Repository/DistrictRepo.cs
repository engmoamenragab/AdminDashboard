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
    public class DistrictRepo : IDistrictRepo
    {
        private readonly AdminDashboardContext adminDashboardDb;

        public DistrictRepo(AdminDashboardContext adminDashboardDb)
        {
            this.adminDashboardDb = adminDashboardDb;
        }

        public IEnumerable<District> Get(Expression<Func<District, bool>> filter = null)
        {
            if(filter == null)
            {
                var data = adminDashboardDb.District.Select(D => D);
                return data;
            }
            else
            {
                return adminDashboardDb.District.Where(filter);
            }
        }
    }
}
