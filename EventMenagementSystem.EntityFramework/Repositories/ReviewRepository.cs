using EventManagementSystem.Model;
using EventMenagementSystem.EntityFramework.Context;
using EventMenagementSystem.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventManagementSystem.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Review> GetByIdAsync(int userId, int eventId)
        {
            return await _context.Reviews
                .Where(r => r.UserID == userId && r.EventID == eventId)
                .FirstOrDefaultAsync();
        }




    }
}



