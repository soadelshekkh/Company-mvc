using DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace MVCProject.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name of Employee is Required")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 char")]
        [MinLength(3, ErrorMessage = "min legth is 3 char")]
        public string Name { get; set; }
        [Range(18, 60, ErrorMessage = "Enter Valid Range beetween 18 to 50 ")]
        public int? Age { get; set; }
        [Range(6000, 50000, ErrorMessage = "salary range is between 6000 to 50000")]
        [DataType(DataType.Currency)]
        public decimal salary { get; set; }
        //@ for skip meaning of spcail char
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{3,15}-[a-zA-Z]{3,15}-[a-zA-Z]{3,15}$",
            ErrorMessage = "Adress must be like 123-street-city-Country")]
        public string Address { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "Enter Valid Email")]
        public string Email { get; set; }
        [Phone]
        public string phoneNumber { get; set; }
        public DateTime HirringDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [ForeignKey("department")]
        public int? DepartmentId { get; set; }
        public Department department { get; set; }
        public IFormFile Image { get; set; } 
        public string ImageName { get; set; }
    }
}
