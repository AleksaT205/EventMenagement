using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventManagementSystem.Model;
using EventMenagementSystem.EntityFramework.Repositories;

namespace EventManagementSystem.Repositories
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        Task<Review> GetByIdAsync(int userId, int eventId);
        Task<IEnumerable<Review>> GetAllAsync();
        void Add(Review entity);
        void Update(Review entity);
        void Delete(Review entity);
        Task<IEnumerable<Review>> Find(Expression<Func<Review, bool>> predicate);
        Task<int> SaveChangesAsync();
    }
}
