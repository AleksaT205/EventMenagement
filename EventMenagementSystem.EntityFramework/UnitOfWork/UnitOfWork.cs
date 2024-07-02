using System;
using System.Threading.Tasks;
using EventManagementSystem.Repositories;
using EventMenagementSystem.EntityFramework.Context;
using EventMenagementSystem.EntityFramework.Repositories;

namespace EventManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public IUserRepository UserRepository { get; }
        public IEventRepository EventRepository { get; }
        public IReviewRepository ReviewRepository { get; }

        private IUserEventRepository _userEventRepository;

        public IUserEventRepository UserEventRepository => _userEventRepository ??= new UserEventRepository(_context);

        public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository, IEventRepository eventRepository, IReviewRepository reviewRepository)
        {
            _context = context;
            UserRepository = userRepository;
            EventRepository = eventRepository;
            ReviewRepository = reviewRepository;
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}









