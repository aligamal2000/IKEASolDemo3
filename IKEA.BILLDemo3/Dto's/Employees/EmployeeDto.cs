using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Commen.Enums;

namespace IKEA.BILLDemo3.Dto_s.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null;
        public int? Age { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public string Gender { get; set; } = null!;
        public EmployeeType EmployeeType { get; set; }
        public string? Department {  get; set; }
    }
}
