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
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto GetDepartmentByid(int id);
        int CreateDepartment(DALDemo3.Models.Departments.CreatedDepartmentDto departmentDto);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
    }
}