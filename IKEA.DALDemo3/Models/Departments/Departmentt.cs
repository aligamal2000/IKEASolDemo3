using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Models.Empolyees;
namespace IKEA.DALDemo3.Models.Departments
{
    public class Departmentt:ModelBase
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
         public virtual ICollection<Employeee> Employees { get; set; }  = new HashSet<Employeee>();
    }
}
