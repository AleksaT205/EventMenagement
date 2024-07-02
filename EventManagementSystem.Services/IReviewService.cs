using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementSystem.Model;

namespace EventManagementSystem.Services
{
    public interface IReviewService
    {
        Task<Review> GetReview(int userId, int eventId);
        Task<IEnumerable<Review>> GetEventReviews(int eventId);
        Task<Review?> AddReview(ReviewDTO reviewDto);
        Task DeleteReview(int userId, int eventId);
    }
}

