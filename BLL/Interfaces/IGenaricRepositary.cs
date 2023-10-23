using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGenaricRepositary <t> where t : class
    {
        public IEnumerable<t> GetAll();
        t GetById(int id);
        int update(t employee);
        int delete(t employee);
        int Add(t employee);
    }
}
