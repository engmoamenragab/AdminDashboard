using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        [MinLength(2, ErrorMessage = "Min Length 2")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code Required")]
        [Range(1, 1000, ErrorMessage = "Code Must be Betwwen 1 To 1000")]
        public string Code { get; set; }
    }
}
