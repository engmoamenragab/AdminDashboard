using ADB.BL.Interfaces;
using ADB.DAL.Database;
using ADB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Repository
{
    public class CountryRepo : ICountryRepo
    {
        private readonly AdminDashboardContext adminDashboardDb;

        public CountryRepo(AdminDashboardContext adminDashboardDb)
        {
            this.adminDashboardDb = adminDashboardDb;
        }

        public IEnumerable<Country> Get()
        {
            var data = adminDashboardDb.Country.Select(C => C);
            return data;
        }
    }
}
