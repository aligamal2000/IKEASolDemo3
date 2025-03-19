﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DALDemo3.Models.Departments
{
    public class CreatedDepartmentDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;
        public string? Description { get; set; }

        public DateOnly CreationDate { get; set; }
    }
}
