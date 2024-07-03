using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq.Expressions;
=======
using System.Linq;
using System.Linq.Expressions;
using System.Text;
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
using System.Threading.Tasks;

namespace EventMenagementSystem.EntityFramework.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<int> SaveChangesAsync();
    }
}
