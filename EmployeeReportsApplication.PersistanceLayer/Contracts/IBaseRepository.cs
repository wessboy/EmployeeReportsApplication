using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReportsApplication.PersistanceLayer.Contracts
{
     public interface IBaseRepository<T>
    {
        IEnumerable<T> FindALL();
        T? FindOne(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
    }
}
