using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Models.Departments;
using IKEA.DALDemo3.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DALDemo3.Persistance.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {

        public ApplictaonDbContext? context {  get; set; }
        private readonly ApplictaonDbContext dbContext;
        public DepartmentRepository(ApplictaonDbContext context){
            dbContext = context;
            //context - new ApplictaonDbContext
            }
        public IEnumerable<Department> GetAll(bool WithNoTracking = true)
        {
            if(WithNoTracking)
                return dbContext.Departments.AsNoTracking().ToList();
            return dbContext.Departments.ToList();
        }
        public Department? GetById(int id)
        {
            var Department = dbContext.Departments.Find(id);
            //var Department = dbContext.Departments.Local.FirstOrDefault(D=>D.Id == id);
            //if(Department == null)
            //    Department = dbContext.Departments.FirstOrDefault(D=>D.Id == id);
            return Department;
        }

 
        public int Add(Department department)
        {
            dbContext.Departments.Add(department);
            return dbContext.SaveChanges();
        }
        public int Update(Department department)
        {
            dbContext.Update(department);
            return dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            dbContext.Departments.Remove(department);
            return dbContext.SaveChanges(); 
        }

    }
}
