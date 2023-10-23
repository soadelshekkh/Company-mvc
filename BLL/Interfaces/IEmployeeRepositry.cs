using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmployeeRepositry :IGenaricRepositary<Employee>
    {
        public IQueryable GetEmployeeByDepartmentName(string departmentName);
        public IQueryable<Employee> GetEmployeeByEmployeeName(string employeeName);   
    }
}
