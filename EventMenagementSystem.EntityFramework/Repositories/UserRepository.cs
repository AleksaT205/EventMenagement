using EventManagementSystem.Model;
using EventMenagementSystem.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventManagementSystem.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

<<<<<<< HEAD
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserWithEventsAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.UserEvents)
                .ThenInclude(ue => ue.Event)
                .FirstOrDefaultAsync(u => u.ID == userId);
        }
    }
}



=======

    }
}
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
