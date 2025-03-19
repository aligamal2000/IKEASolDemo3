using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Models.Departments;

namespace IKEA.DALDemo3.Persistance.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool WithNoTracking = true);
        Department ? GetById(int id);

        int Add(Department department);
        int Update(Department department);
        int Delete(Department department);
    }
}

