using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BILLDemo3.Dto_s.Departments;
using IKEA.DALDemo3.Models.Departments;

namespace IKEA.BILLDemo3.Services.DepartmentServices
{
    public interface IDepartmentServices
    {
        //services
        Task <IEnumerable<DepartmentDto>> GetAllDepartments();
      Task  <DepartmentDetailsDto> GetDepartmentByid(int id);
        Task<int> CreateDepartment(DALDemo3.Models.Departments.CreatedDepartmentDto departmentDto);
        Task<int> UpdateDepartment(UpdatedDepartmentDto departmentDto);
        Task<bool> DeleteDepartment(int id);
    }
}