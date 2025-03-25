using AutoMapper;
using IKEA.BILLDemo3.Dto_s.Departments;
using IKEA.PLDemo3.Models;

namespace IKEA.PLDemo3.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEditDepartmentVM, CreatedDepartmentDto>(). ReverseMap();
            CreateMap<DepartmentDetailsDto, CreateEditDepartmentVM>().ReverseMap();
            CreateMap<CreateEditDepartmentVM,UpdatedDepartmentDto>().ReverseMap();

            //.ForMember(dest => dest.Name, config => config.MapFrom(src => src.Name));
        }


    }
}

