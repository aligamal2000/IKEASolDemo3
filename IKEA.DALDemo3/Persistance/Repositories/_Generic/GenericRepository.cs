using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Models;
using IKEA.DALDemo3.Models.Empolyees;
using IKEA.DALDemo3.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DALDemo3.Persistance.Repositories._Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        public ApplictaonDbContext? context { get; set; }
        private readonly ApplictaonDbContext dbContext;
        public GenericRepository(ApplictaonDbContext context)
        {
            dbContext = context;
            //context - new ApplictaonDbContext
        }
        public IEnumerable<T> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking)
                return dbContext.Set<T>().Where(D => D.IsDeleted == false).AsNoTracking().ToList();
            return dbContext.Set<T>().Where(D => D.IsDeleted == false).ToList();
        }
        public T? GetById(int id)
        {
            var item = dbContext.Set<T>().Find(id);
            //var Department = dbContext.Departments.Local.FirstOrDefault(D=>D.Id == id);
            //if(Department == null)
            //    Department = dbContext.Departments.FirstOrDefault(D=>D.Id == id);
            return item;
        }


        public int Add(T item)
        {
            dbContext.Set<T>().Add(item);
            return dbContext.SaveChanges();
        }
        public int Update(T item)
        {
            dbContext.Update(item);
            return dbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            item.IsDeleted = true;
            dbContext.Set<T>().Update(item);
            return dbContext.SaveChanges();
        }
    }
}
