using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Persistance.Data;
using IKEA.DALDemo3.Persistance.Repositories.Departments;
using IKEA.DALDemo3.Persistance.Repositories.Empoyees;

namespace IKEA.DALDemo3.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
            DepartmentRepository = new DepartmentRepository(this.dbContext);
            EmployeeRepository = new EmployeeRepository(this.dbContext);   
        }
        public int Complete()
        {
            return dbContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        //public void Dispose()
        //{
        //    dbContext.Dispose();
        //}
    }
}
