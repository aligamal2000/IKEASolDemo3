using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Models.Departments;
using IKEA.DALDemo3.Models.Empolyees;
using IKEA.DALDemo3.Persistance.Repositories._Generic;

namespace IKEA.DALDemo3.Persistance.Repositories.Empoyees
{
    public interface IEmployeeRepository:IGenericRepository<Employeee>
    {
     
    }
}
