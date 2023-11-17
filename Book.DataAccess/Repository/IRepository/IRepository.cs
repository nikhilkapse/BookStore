using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class // this is generic interface hence added generic type by T
    {
        // T will be category or any other generic model on which we want to perform CRUD operations
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity); 
        void RemoveRange(IEnumerable<T> entities);
    }
}
