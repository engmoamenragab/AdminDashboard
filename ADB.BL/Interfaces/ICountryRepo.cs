using ADB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Interfaces
{
    public interface ICountryRepo
    {
        IEnumerable<Country> Get();
    }
}
