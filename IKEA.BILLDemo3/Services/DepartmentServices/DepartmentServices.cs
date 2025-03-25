using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BILLDemo3.Dto_s.Departments;
using IKEA.DALDemo3.Models.Departments;
using IKEA.DALDemo3.Models.Empolyees;
using IKEA.DALDemo3.Persistance.Repositories.Departments;
using IKEA.DALDemo3.Persistance.UnitOfWork;

namespace IKEA.BILLDemo3.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfWork unitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            unitOfWork = unitOfWork;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var Departments = unitOfWork.DepartmentRepository.GetAll().Where(D => !D.IsDeleted).Select(dept => new DepartmentDto()
            {
                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate,   

            }).ToList();
            return Departments;
            //List<DepartmentDto>departmentDtos = new List<DepartmentDto>();
            //foreach (var dept in Deparments)
            //{
            //   DepartmentDto departmentDto = new DepartmentDto()
            //   { 
            //        Id= dept.Id,
            //       Name = dept.Name,
            //       Code = dept.Code,
            //       CreationDate = dept.CreationDate,


            //   };
            //    departmentDtos.Add(departmentDto);

        }
    
  public DepartmentDetailsDto? GetDepartmentByid(int id)
        {
            var Department = unitOfWork.DepartmentRepository.GetById(id);

            if (Department is not null)
                return new DepartmentDetailsDto()
                {
                    Id = Department.Id,
                    Name = Department.Name,
                    Code = Department.Code,
                    CreationDate = Department.CreationDate,
                    IsDeleted = Department.IsDeleted,
                    LastModifiedBy = Department.LastModifiedBy,
                    LastModifiedOn = Department.LastModifiedOn,
                    CreatedBy = Department.CreatedBy,
                    CreatedOn = Department.CreatedOn,
                };
            return null;

        }


        public int CreateDepartment(DALDemo3.Models.Departments.CreatedDepartmentDto departmentDto)
        {
            var CreatedDepartment = new Departmentt()
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

             unitOfWork.DepartmentRepository.Add(CreatedDepartment);
            return unitOfWork.Complete();

        }
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var UpdatedDepartment = new Departmentt()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };

            unitOfWork.DepartmentRepository.Update(UpdatedDepartment);
            return unitOfWork.Complete();
        }

        public bool DeleteDepartment(int id)
        {
            var department = unitOfWork.DepartmentRepository.GetById(id);
            if (department is not null)
            unitOfWork.DepartmentRepository.Delete(department);
            var result = unitOfWork.Complete();
            if (result > 0)
                return true;
            else
                return false;


        }
    }
}

