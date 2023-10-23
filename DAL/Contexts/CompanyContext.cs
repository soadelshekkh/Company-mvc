using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contexts
{
    public class CompanyContext: IdentityDbContext<UserRegister>
    {
        public CompanyContext(DbContextOptions<CompanyContext> options):base(options)
        {
                
        }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("server = .; database = CompanyForMvc; integrated security =true ");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>(e => e.HasKey(DId => DId.Dnum));
            
        }
    }
}
