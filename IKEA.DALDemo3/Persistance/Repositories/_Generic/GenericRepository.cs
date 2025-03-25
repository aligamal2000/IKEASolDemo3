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
        public ApplicationDbContext? context { get; set; }
        private readonly ApplicationDbContext dbContext;
        public GenericRepository(ApplicationDbContext context)
        {
            dbContext = context;
            //context - new ApplictaonDbContext
        }
        public IQueryable<T> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking)
                return dbContext.Set<T>().AsNoTracking();
            return dbContext.Set<T>();
        }
        public T? GetById(int id)
        {
            var item = dbContext.Set<T>().Find(id);
            //var Department = dbContext.Departments.Local.FirstOrDefault(D=>D.Id == id);
            //if(Department == null)
            //    Department = dbContext.Departments.FirstOrDefault(D=>D.Id == id);
            return item;
        }


        public void Add(T item)
        {
            dbContext.Set<T>().Add(item);
        }
        public void Update(T item)
        {
            dbContext.Update(item);
        }

        public void Delete(T item)
        {
            item.IsDeleted = true;
            dbContext.Set<T>().Update(item);
        }
    }
}
