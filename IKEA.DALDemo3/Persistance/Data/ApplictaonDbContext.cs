using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Commen.Enums;
using IKEA.DALDemo3.Models.Departments;
using IKEA.DALDemo3.Models.Empolyees;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DALDemo3.Persistance.Data
{
    public class ApplictaonDbContext:DbContext
    {
        public ApplictaonDbContext(DbContextOptions options) : base(options)
        {
        }


        // protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        // {
        //    optionBuilder.UseSqlServer("Server =DESKTOP-68C09D0; Database = IKEA_G02; trusted_Connection = true; TrustServerCertificate=true");


        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
