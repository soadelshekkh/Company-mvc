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
    public class GenaricRepositary<t> : IGenaricRepositary<t> where t : class
    {

        private CompanyContext Context;
        public GenaricRepositary(CompanyContext Context)
        {
            this.Context = Context;
        }

        public int Add(t Item)
        {
            Context.Set<t>().Add(Item);
            return Context.SaveChanges();
        }

        public int delete(t Item)
        {
            Context.Set<t>().Remove(Item);
            return Context.SaveChanges();
        }

        public IEnumerable<t> GetAll()
        {
            if (typeof(t) == typeof(Employee))
                return (IEnumerable<t>)Context.Set<Employee>().Include(e => e.department).ToList();
            return Context.Set<t>().ToList();
        }

        public t GetById(int id)
        {
            //return Context.Set<t>().Where(E => E.Id == id).FirstOrDefault();
            return Context.Set<t>().Find(id);
        }

        public int update(t Item)
        {
            Context.Set<t>().Update(Item); 
            return Context.SaveChanges();
        }
    }
}
