using ADB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Interfaces
{
    public interface ICityRepo
    {
        IEnumerable<City> Get(Expression<Func<City, bool>> filter = null);
    }
}
