using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventManagementSystem.Model;

namespace EventManagementSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        void Add(User entity);
        void Update(User entity);
        void Delete(User entity);
        Task<IEnumerable<User>> Find(Expression<Func<User, bool>> predicate);
        Task<int> SaveChangesAsync();
    }
}

