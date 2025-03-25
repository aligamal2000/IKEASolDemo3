using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BILLDemo3.Dto_s.Employees;
using IKEA.DALDemo3.Models.Empolyees;
using IKEA.DALDemo3.Persistance.Repositories.Empoyees;
using IKEA.DALDemo3.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BILLDemo3.Services.EmployeeServices
{
    public class EmployeeServices:IEmpolyeeServices
    {
        private readonly IEmployeeRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public EmployeeServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<EmployeeDto> GetAllEmployees(string search)
        {
            var Employees = unitOfWork.EmployeeRepository.GetAll();

            var QueryEmployees = Employees
       .Where(E => !E.IsDeleted &&(string.IsNullOrEmpty(search)||E.Name.ToLower().Contains(search.ToLower()))).Include(E=>E.Department)
       .Select(E => new EmployeeDto()
       {
           Id = E.Id,
           Name = E.Name,
           Age = E.Age,
           Salary = E.Salary,
           IsActive = E.IsActive,
           Email = E.Email,
           Gender = E.Gender.ToString(),
           EmployeeType = E.EmployeeType,
           Department = E.Department.Name ?? "N/A"
       }).ToList();

            var FirstEmployee = QueryEmployees.FirstOrDefault(); // Prevents exception
            return QueryEmployees;
        }


        public EmployeeDetailsDto GetEmployeeById(int id)
        {
           var employee = unitOfWork.EmployeeRepository.GetById(id);
            if (employee is not null)
            {
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    DepartmentId = employee.DepartmentId,
                    LastModifiedBy = employee.LastModifiedBy,
                    CreatedBy = employee.CreatedBy,
                    LastModifiedOn = employee.LastModifiedOn, 
                    CreatedOn = employee.CreatedOn,
                    Department = employee.Department.Name ?? "N/A"

                };
            }
            return new EmployeeDetailsDto();
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employeee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
                CreatedOn = DateTime.Now,
            };
            unitOfWork.EmployeeRepository.Add(employee);
            return unitOfWork.Complete();
        }
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employeee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
        
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
   
            };
            unitOfWork.EmployeeRepository.Update(employee);
            return unitOfWork.Complete();
        }
        public bool DeleteEmployee(int id)
        {
            var employee = unitOfWork.EmployeeRepository.GetById(id);

            if (employee is not null)
                 unitOfWork.EmployeeRepository.Delete(employee);
            var result = unitOfWork.Complete();
            if(result>0)
                return true;
                else
                return false;

        }



      

 
    }
}
