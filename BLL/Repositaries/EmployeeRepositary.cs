using BLL.Interfaces;
using DAL.Contexts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositaries
{
    public class EmployeeRepositary : GenaricRepositary<Employee> ,IEmployeeRepositry
    {
        private CompanyContext Context;
        public EmployeeRepositary(CompanyContext Context):base(Context)
        {
            this.Context= Context;
        }

        public IQueryable GetEmployeeByDepartmentName(string departmentName)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> GetEmployeeByEmployeeName(string employeeName)
        {
            return Context.Employees.Where(E => E.Name.Contains(employeeName));
        }
    }
}
