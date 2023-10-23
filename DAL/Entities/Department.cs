using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Department
    { 
        public int Dnum { get; set; }
        [MaxLength(50,ErrorMessage ="Name max length is 50")]
        [Required(ErrorMessage ="Name is Required")]
        public string DName { get; set; }  
        public DateTime HirringDate { get; set; }
        public DateTime DateOfCreation { get; set; }
        public ICollection<Employee> Employees { get; set; }  = new HashSet<Employee>();
    }
}
