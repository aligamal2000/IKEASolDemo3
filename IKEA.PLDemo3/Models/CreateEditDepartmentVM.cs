using System.ComponentModel.DataAnnotations;

namespace IKEA.PLDemo3.Models
{
    public class CreateEditDepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The name is Required")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "the code is Requried")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = "Date of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
