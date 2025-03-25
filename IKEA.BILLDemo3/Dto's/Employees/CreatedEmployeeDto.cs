using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Commen.Enums;


namespace IKEA.BILLDemo3.Dto_s.Employees
{
    public class CreatedEmployeeDto
    {

        [MaxLength(50, ErrorMessage ="Max length 50")]
        [MinLength(5, ErrorMessage = "Min length 5")]
        public string? Name { get; set; }
        [Range(22,23)]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
    ErrorMessage = "Address must be like 123-Street-City-Country")]

        public string? Address { get; set; }
        public decimal Salary { get; set; }
        [Display(Name="is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name="Phone no")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Display(Name="Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [Display(Name="Department")]
        public int? DepartmentId { get; set; }

    }
}
