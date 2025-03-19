using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BILLDemo3.Dto_s.Departments
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = "Date of Creation")]
        public DateOnly CreationDate { get; set; }

    }
}
