using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DALDemo3.Models;
using IKEA.DALDemo3.Models.Departments;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IKEA.DALDemo3.Persistance.Repositories._Generic
{
    public interface IGenericRepository< T> where T : ModelBase
    {
        IQueryable<T> GetAll(bool WithNoTracking = true);
        T? GetById(int id);

        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);

    }
}
