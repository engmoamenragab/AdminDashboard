using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Models
{
    public class RoleVM
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
