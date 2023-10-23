using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IdepartmentRepositary
    {
        public IEnumerable<Department> GetAll();
        Department GetById(int id);
        int updateDepartment(Department department);
        int deleteDepartment(Department department);
        int AddDeparment(Department department);
    }
}
