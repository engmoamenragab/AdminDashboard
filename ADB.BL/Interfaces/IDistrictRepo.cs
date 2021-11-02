using ADB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Interfaces
{
    public interface IDistrictRepo
    {
        IEnumerable<District> Get(Expression<Func<District, bool>> filter = null);
    }
}
