<<<<<<< HEAD
﻿using EventManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventManagementSystem.Model;
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b

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
<<<<<<< HEAD
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetUserWithEventsAsync(int userId);
    }
}



=======
    }
}

>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
