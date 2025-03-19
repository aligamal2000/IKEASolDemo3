using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BILLDemo3.Dto_s.Departments;
using IKEA.DALDemo3.Models.Departments;

namespace IKEA.BILLDemo3.Services.DepartmentServices
{
    public interface IDepartmentServices
    {
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto GetDepartmentByid(int id);
        int CreateDepartment(DALDemo3.Models.Departments.CreatedDepartmentDto departmentDto);
        int UpdateDepartment(UpdatedDeparmentDto departmentDto);
        bool DeleteDepartment(int id);  
    }
}
