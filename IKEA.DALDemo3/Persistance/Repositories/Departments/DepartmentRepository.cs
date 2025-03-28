﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Models.Departments;
using IKEA.DALDemo3.Persistance.Data;
using IKEA.DALDemo3.Persistance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DALDemo3.Persistance.Repositories.Departments
{
    public class DepartmentRepository :GenericRepository<Department>, IDepartmentRepository
    {
        public ApplictaonDbContext? context { get; set; }
        private readonly ApplictaonDbContext dbContext;
        public DepartmentRepository(ApplictaonDbContext context):base(context)
        {
            dbContext = context;
            //context - new ApplictaonDbContext
        }


    }
}
