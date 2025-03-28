﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Commen.Enums;
using IKEA.DALDemo3.Models.Empolyees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKEA.DALDemo3.Persistance.Data.Configurations.EmpoyleeConfirgurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employeee>
    {
        public void Configure(EntityTypeBuilder<Employeee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();

            builder.Property(E => E.Address).HasColumnType("varchar(100)");

            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");

            builder.Property(e => e.Gender).HasConversion
            (
               ( gender) => gender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
            );
            builder.Property(e => e.EmployeeType).HasConversion
      (
          (Type) => Type.ToString(),
          (Type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), Type)
      );
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GetDate()");

        }
    }
}
