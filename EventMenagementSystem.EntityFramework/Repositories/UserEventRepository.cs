using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventManagementSystem.Model;
using EventManagementSystem.Repositories;
using EventMenagementSystem.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace EventMenagementSystem.EntityFramework.Repositories
{
    public class UserEventRepository : BaseRepository<UserEvent>, IUserEventRepository
    {
        public UserEventRepository(ApplicationDbContext context) : base(context) { }

        public async Task<UserEvent> GetByConditionAsync(Expression<Func<UserEvent, bool>> predicate)
        {
            return await _context.UserEvents.FirstOrDefaultAsync(predicate);
        }
    }
}

