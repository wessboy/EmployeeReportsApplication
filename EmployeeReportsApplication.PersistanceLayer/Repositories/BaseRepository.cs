using EmployeeReportsApplication.PersistanceLayer.Contracts;
using EmployeeReportsApplication.PersistanceLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;


namespace EmployeeReportsApplication.PersistanceLayer.Repositories
{
     public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        internal DbSet<T> dbSet;

        protected BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            this.dbSet = _applicationDbContext.Set<T>();
        }
        public void Create(T entity)
        {
            _applicationDbContext.Add(entity); 
            _applicationDbContext.SaveChanges();
            
        }

        public IEnumerable<T> FindALL()
        {
            IQueryable<T> query = dbSet;

            return query.ToList();
        }

        public T? FindOne(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = dbSet;

            return query.FirstOrDefault(expression);    
        }

        public void Update(T entity)
        {
            _applicationDbContext.Update(entity);
            _applicationDbContext.SaveChanges();    
        }
    }
}
