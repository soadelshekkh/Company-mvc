using BLL.Interfaces;
using DAL.Contexts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositaries
{
    public class DepartmentRepositary : IdepartmentRepositary
    {
        private readonly CompanyContext context;
        public DepartmentRepositary(CompanyContext context)
        {
            this.context = context;
        }

        public int AddDeparment(Department department)
        {
            context.Add(department);
            return context.SaveChanges();
            
        }

        public int deleteDepartment(Department department)
        {
            context.Department.Remove(department);
            return context.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return context.Department.ToList();
        }

        public Department GetById(int id)
        {
           return context.Department.Where(d => d.Dnum == id).FirstOrDefault();
        }

        public int updateDepartment(Department department)
        {
            context.Department.Update(department);
            return context.SaveChanges();
        }
    }
}
