using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int? Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal salary { get; set; }
        //@ for skip meaning of spcail char
        public string Address { get; set; }
        public string Email { get; set; }
        public string  phoneNumber { get; set; }
        public DateTime HirringDate { get; set; }
        //public bool IsDeleted { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [ForeignKey("department")]
        public int? DepartmentId { get; set; }
        public Department department { get; set; }
        public string ImageName { get; set; }
    }

}
