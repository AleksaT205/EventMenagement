<<<<<<< HEAD
﻿using System;
using System.Threading.Tasks;
using EventManagementSystem.Repositories;
using EventMenagementSystem.EntityFramework.Context;
using EventMenagementSystem.EntityFramework.Repositories;

namespace EventManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
=======
﻿using EventManagementSystem.Repositories;
using EventMenagementSystem.EntityFramework.Context;
using System;
using System.Threading.Tasks;

namespace EventManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public IUserRepository UserRepository { get; }
        public IEventRepository EventRepository { get; }
        public IReviewRepository ReviewRepository { get; }

<<<<<<< HEAD
        private IUserEventRepository _userEventRepository;

        public IUserEventRepository UserEventRepository => _userEventRepository ??= new UserEventRepository(_context);

        public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository, IEventRepository eventRepository, IReviewRepository reviewRepository)
=======
        public UnitOfWork(
            ApplicationDbContext context,
            IUserRepository userRepository,
            IEventRepository eventRepository,
            IReviewRepository reviewRepository)
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
        {
            _context = context;
            UserRepository = userRepository;
            EventRepository = eventRepository;
            ReviewRepository = reviewRepository;
        }

<<<<<<< HEAD
        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
=======

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
<<<<<<< HEAD
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
=======

                _disposed = true;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
        }
    }
}




<<<<<<< HEAD





=======
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
