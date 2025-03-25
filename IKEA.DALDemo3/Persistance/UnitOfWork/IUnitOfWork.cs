using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Persistance.Repositories.Departments;
using IKEA.DALDemo3.Persistance.Repositories.Empoyees;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DALDemo3.Persistance.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get;  }
        int Complete();
        
        
         void Dispose();
        
    }
}
