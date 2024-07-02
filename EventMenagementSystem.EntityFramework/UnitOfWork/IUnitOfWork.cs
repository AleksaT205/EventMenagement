using System;
using System.Threading.Tasks;
using EventManagementSystem.Repositories;
using EventMenagementSystem.EntityFramework.Repositories;

namespace EventManagementSystem.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IEventRepository EventRepository { get; }
        IReviewRepository ReviewRepository { get; }

        IUserEventRepository UserEventRepository { get; } // Dodaj ovu liniju

        void Update<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}





