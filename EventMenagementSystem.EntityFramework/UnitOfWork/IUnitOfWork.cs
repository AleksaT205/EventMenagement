using EventManagementSystem.Repositories;
using System;
using System.Threading.Tasks;

namespace EventManagementSystem.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IEventRepository EventRepository { get; }
        IReviewRepository ReviewRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
