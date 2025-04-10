using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IKEA.BILLDemo3.Commen.Services.Attachments;
using IKEA.BILLDemo3.Dto_s.Employees;
using IKEA.DALDemo3.Models.Empolyees;
using IKEA.DALDemo3.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BILLDemo3.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAttachmentServices attachmentServices;

        public EmployeeServices(IUnitOfWork unitOfWork, IAttachmentServices attachmentServices)
        {
            this.unitOfWork = unitOfWork;
            this.attachmentServices = attachmentServices;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees(string search)
        {
            var employees =  unitOfWork.EmployeeRepository.GetAll();

            var queryEmployees = employees
                .Where(e => !e.IsDeleted && (string.IsNullOrEmpty(search) || e.Name.ToLower().Contains(search.ToLower())))
                .Include(e => e.Department)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age,
                    Salary = e.Salary,
                    IsActive = e.IsActive,
                    Email = e.Email,
                    Gender = e.Gender.ToString(),
                    EmployeeType = e.EmployeeType,
                    Department = e.Department != null ? e.Department.Name : "N/A"
                })
                .ToListAsync();

            return await queryEmployees;
        }

        public async Task<EmployeeDetailsDto> GetEmployeeById(int id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetById(id);
            if (employee != null)
            {
                return new EmployeeDetailsDto
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
                    Department = employee.Department?.Name,
                    ImageName = employee.ImageName
                };
            }

            return null;
        }

        public async Task<int> CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employeee
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
                CreatedOn = DateTime.Now
            };

            if (employeeDto.Image != null)
            {
                employee.ImageName = attachmentServices.UploadImage(employeeDto.Image, "images");
            }

            unitOfWork.EmployeeRepository.Add(employee);
            return await unitOfWork.Complete();
        }

        public async Task<int> UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = await unitOfWork.EmployeeRepository.GetById(employeeDto.Id);
            if (employee == null)
                return 0;

            employee.Name = employeeDto.Name;
            employee.Age = employeeDto.Age;
            employee.Address = employeeDto.Address;
            employee.IsActive = employeeDto.IsActive;
            employee.Salary = employeeDto.Salary;
            employee.Email = employeeDto.Email;
            employee.PhoneNumber = employeeDto.PhoneNumber;
            employee.HiringDate = employeeDto.HiringDate;
            employee.Gender = employeeDto.Gender;
            employee.EmployeeType = employeeDto.EmployeeType;
            employee.LastModifiedBy = 1;
            employee.LastModifiedOn = DateTime.Now;

            if (employeeDto.Image != null)
            {
                if (!string.IsNullOrEmpty(employee.ImageName))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", employee.ImageName);
                    attachmentServices.DeleteImage(filePath);
                }

                employee.ImageName = attachmentServices.UploadImage(employeeDto.Image, "images");
            }

            unitOfWork.EmployeeRepository.Update(employee);
            return await unitOfWork.Complete();
        }

        public async Task <bool> DeleteEmployee(int id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetById(id);
            if (employee == null)
                return false;

            if (!string.IsNullOrEmpty(employee.ImageName))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", employee.ImageName);
                attachmentServices.DeleteImage(filePath);
            }

            unitOfWork.EmployeeRepository.Delete(employee);
            return await unitOfWork.Complete() > 0;
        }
    }
}
