using EventManagementSystem.Repositories;
using EventMenagementSystem.EntityFramework.Context;
using System;
using System.Threading.Tasks;

namespace EventManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public IUserRepository UserRepository { get; }
        public IEventRepository EventRepository { get; }
        public IReviewRepository ReviewRepository { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IUserRepository userRepository,
            IEventRepository eventRepository,
            IReviewRepository reviewRepository)
        {
            _context = context;
            UserRepository = userRepository;
            EventRepository = eventRepository;
            ReviewRepository = reviewRepository;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}




