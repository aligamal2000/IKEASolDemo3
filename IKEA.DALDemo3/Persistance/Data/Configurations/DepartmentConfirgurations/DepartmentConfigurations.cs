using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Models.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKEA.DALDemo3.Persistance.Data.Configurations.DepartmentConfirgurations
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Departmentt>
    {
        public void Configure(EntityTypeBuilder<Departmentt> builder)
        {
            builder.Property(D=>D.Id).UseIdentityColumn(10,10);
            builder.Property(D => D.Name).HasColumnType("Varchar(50)").IsRequired();
            builder.Property(D => D.Code).HasColumnType("Varchar(50)").IsRequired();
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GetDate()");
            builder.HasMany(D => D.Employees)
       .WithOne(E => E.Department)
       .HasForeignKey(E => E.DepartmentId)
       .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
