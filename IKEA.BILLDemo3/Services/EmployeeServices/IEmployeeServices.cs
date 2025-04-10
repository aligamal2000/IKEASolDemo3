using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BILLDemo3.Dto_s.Departments;
using IKEA.BILLDemo3.Dto_s.Employees;

namespace IKEA.BILLDemo3.Services.EmployeeServices
{
    public interface IEmployeeServices

    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees(string search);
       Task <EmployeeDetailsDto>? GetEmployeeById(int id);
        Task <int> CreateEmployee(CreatedEmployeeDto employeeDto);
        Task<int> UpdateEmployee(UpdatedEmployeeDto employeeDto);
        Task <bool> DeleteEmployee(int id);
    }
}
