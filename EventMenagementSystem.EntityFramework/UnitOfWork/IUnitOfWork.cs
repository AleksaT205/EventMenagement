<<<<<<< HEAD
﻿using System;
using System.Threading.Tasks;
using EventManagementSystem.Repositories;
using EventMenagementSystem.EntityFramework.Repositories;
=======
﻿using EventManagementSystem.Repositories;
using System;
using System.Threading.Tasks;
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b

namespace EventManagementSystem.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IEventRepository EventRepository { get; }
        IReviewRepository ReviewRepository { get; }

<<<<<<< HEAD
        IUserEventRepository UserEventRepository { get; } // Dodaj ovu liniju

        void Update<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}





=======
        Task<int> SaveChangesAsync();
    }
}
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
