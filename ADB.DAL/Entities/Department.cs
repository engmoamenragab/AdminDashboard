using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADB.DAL.Entities
{
    [Table("Department")]
    public class Department
    {
        // To make the key without Identity
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public ICollection<Employee> Employee { get; set; }
    }
}
