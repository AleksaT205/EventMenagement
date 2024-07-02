using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventManagementSystem.Model;

namespace EventMenagementSystem.EntityFramework.Repositories
{
    public interface IUserEventRepository : IBaseRepository<UserEvent>
    {
        Task<UserEvent> GetByConditionAsync(Expression<Func<UserEvent, bool>> predicate);
    }
}

