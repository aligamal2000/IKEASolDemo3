using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BILLDemo3.Dto_s.Departments;
using IKEA.BILLDemo3.Dto_s.Employees;

namespace IKEA.BILLDemo3.Services.EmployeeServices
{
    public interface IEmpolyeeServices
    {
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDetailsDto GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
    }
}
