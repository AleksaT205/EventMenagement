using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementSystem.Model;
using EventManagementSystem.Repositories;
using EventManagementSystem.UnitOfWork;

namespace EventManagementSystem.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Review> GetReview(int userId, int eventId)
        {
            return await _unitOfWork.ReviewRepository.GetByIdAsync(userId, eventId);
        }

        public async Task<IEnumerable<Review>> GetEventReviews(int eventId)
        {
            return await _unitOfWork.ReviewRepository.Find(r => r.EventID == eventId);
        }

        /*public async Task<Review?> AddReview(int userId, int eventId, int rating, string comment)
        {
            try
            {
                // Pronađi korisnika i događaj
                var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
                var evnt = await _unitOfWork.EventRepository.GetByIdAsync(eventId);

                // Provera da li su pronađeni korisnik i događaj
                if (user == null || evnt == null)
                {
                    return null; // Ako korisnik ili događaj nisu pronađeni, ne možemo dodati recenziju
                }

                // Kreiranje recenzije
                var review = new Review
                {
                    Rating = rating,
                    Comment = comment,
                    UserID = userId,
                    EventID = eventId,
                    User = user,
                    Event = evnt
                };

                // Dodavanje recenzije u repozitorijum
                _unitOfWork.ReviewRepository.Add(review);
                await _unitOfWork.SaveChangesAsync();

                // Opciono: Dodavanje recenzije u listu recenzija događaja (Event)
                evnt.Reviews.Add(review);
                await _unitOfWork.SaveChangesAsync(); // Sačuvaj promene za događaj

                return review;
            }
            catch (Exception ex)
            {
                // Tretiraj izuzetak prema potrebi
                Console.WriteLine($"Error adding review: {ex.Message}");
                return null;
            }
        }*/

        public async Task<Review?> AddReview(ReviewDTO reviewDto)
        {
            try
            {
                // Pronađi korisnika i događaj
                var user = await _unitOfWork.UserRepository.GetByIdAsync(reviewDto.UserId);
                var evnt = await _unitOfWork.EventRepository.GetByIdAsync(reviewDto.EventId);

                // Provera da li su pronađeni korisnik i događaj
                if (user == null || evnt == null)
                {
                    return null; // Ako korisnik ili događaj nisu pronađeni, ne možemo dodati recenziju
                }

                // Kreiranje recenzije
                var review = new Review
                {
                    Rating = reviewDto.Rating,
                    Comment = reviewDto.Comment,
                    UserID = reviewDto.UserId,
                    EventID = reviewDto.EventId,
                    User = user,
                    Event = evnt
                };

                // Dodavanje recenzije u repozitorijum
                _unitOfWork.ReviewRepository.Add(review);
                await _unitOfWork.SaveChangesAsync();

                // Opciono: Dodavanje recenzije u listu recenzija događaja (Event)
                evnt.Reviews.Add(review);
                await _unitOfWork.SaveChangesAsync(); // Sačuvaj promene za događaj

                return review;
            }
            catch (Exception ex)
            {
                // Tretiraj izuzetak prema potrebi
                Console.WriteLine($"Error adding review: {ex.Message}");
                return null;
            }
        }



        public async Task DeleteReview(int userId, int eventId)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(userId, eventId);
            if (review != null)
            {
                _unitOfWork.ReviewRepository.Delete(review);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
