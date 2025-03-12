using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Persistance.Repositories.Departments;

namespace IKEA.BILLDemo3.Services.DepartmentServices
{
    public class DepartmentServices:IDepartmentServices
    {
       private IDepartmentRepository Repository;
        public DepartmentServices(IDepartmentRepository _repository)
        {
            Repository = _repository;   

        }
         
        



    }
}
