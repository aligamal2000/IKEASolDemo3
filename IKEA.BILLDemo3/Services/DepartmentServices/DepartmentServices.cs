using System;
using System.Collections.Generic;
using System.Linq;
using IKEA.BILLDemo3.Dto_s.Departments;
using IKEA.DALDemo3.Models.Departments;
using IKEA.DALDemo3.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BILLDemo3.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfWork unitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartments()
        {
            if ( unitOfWork.DepartmentRepository == null)
                throw new InvalidOperationException("DepartmentRepository is not initialized.");

            var departments =    unitOfWork.DepartmentRepository.GetAll();

            if (departments == null)
                throw new InvalidOperationException("DepartmentRepository.GetAll() returned null.");

            var departmentDtos =  await departments
                .Where(d => d != null && !d.IsDeleted)
                .Select(dept => new DepartmentDto
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    Code = dept.Code,
                    CreationDate = dept.CreationDate
                })
                .ToListAsync();

            return departmentDtos;
        }

        public async Task <DepartmentDetailsDto>? GetDepartmentByid(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetById(id);

            if (department is not null)
                return new DepartmentDetailsDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    CreationDate = department.CreationDate,
                    IsDeleted = department.IsDeleted,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                };

            return null;
        }

        public async  Task<int> CreateDepartment(DALDemo3.Models.Departments.CreatedDepartmentDto departmentDto)
        {
            var createdDepartment = new Departmentt
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now
            };

            unitOfWork.DepartmentRepository.Add(createdDepartment);
            return  await unitOfWork.Complete();
        }

        public async  Task<int> UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var updatedDepartment = new Departmentt
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };

            unitOfWork.DepartmentRepository.Update(updatedDepartment);
            return  await unitOfWork.Complete();
        }

        public async Task <bool> DeleteDepartment(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetById(id);
            if (department != null)
            {
                unitOfWork.DepartmentRepository.Delete(department);
                var result = await unitOfWork.Complete();
                return  result > 0;
            }

            return false;
        }
    }
}
