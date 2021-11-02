using ADB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Models
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        [MinLength(2, ErrorMessage = "Min Length 2")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Salary Required")]
        [Range(2000, 10000, ErrorMessage = "Salary Range Between 2000 : 10000")]
        public double Salary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        // [Building Number]-[Street]-[City]-[Country]
        [RegularExpression("[0-9]{2,5}-[a-zA-Z]{3,10}-[a-zA-Z]{3,10}-[a-zA-Z]{3,10}", ErrorMessage = "Address Pattern Like This : [Building Number]-[Street]-[City]-[Country]")]
        public string Address { get; set; }
        public string Notes { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Email Invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Choose Department")]
        public int DepartmentId { get; set; }
        public int DistrictId { get; set; }
        public Department Department { get; set; }
        public District District { get; set; }

        public EmployeeVM()
        {
            CreationDate = DateTime.Now;
        }
    }
}
