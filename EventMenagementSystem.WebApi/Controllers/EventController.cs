using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Model;
using EventManagementSystem.Repositories;
using EventManagementSystem.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Event/Users
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return Ok(users);
        }

        // GET: api/Event/Events
        [HttpGet("Events")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _unitOfWork.EventRepository.GetAllAsync();
            return Ok(events);
        }

        // POST: api/Event/CreateUser
        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.ID }, user);
        }

        // POST: api/Event/CreateEvent
        [HttpPost("CreateEvent")]
        public async Task<ActionResult<Event>> CreateEvent(Event evnt)
        {
            _unitOfWork.EventRepository.Add(evnt);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvents), new { id = evnt.ID }, evnt);
        }

        // POST: api/Event/AddReview
        [HttpPost("AddReview")]
        public async Task<ActionResult<Review>> AddReview(int userId, int eventId, Review review)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            var evnt = await _unitOfWork.EventRepository.GetByIdAsync(eventId);

            if (user == null || evnt == null)
            {
                return BadRequest("User or Event not found.");
            }

            review.UserID = userId;
            review.EventID = eventId;

            _unitOfWork.ReviewRepository.Add(review);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReview), new { userId, eventId }, review);
        }

        // DELETE: api/Event/DeleteReview/5
        [HttpDelete("DeleteReview/{userId}/{eventId}")]
        public async Task<IActionResult> DeleteReview(int userId, int eventId)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(userId, eventId);

            if (review == null)
            {
                return NotFound();
            }

            _unitOfWork.ReviewRepository.Delete(review);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Event/GetReview
        [HttpGet("GetReview")]
        public async Task<ActionResult<Review>> GetReview(int userId, int eventId)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(userId, eventId);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // GET: api/Event/GetEventReviews/{eventId}
        [HttpGet("GetEventReviews/{eventId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetEventReviews(int eventId)
        {
            var reviews = await _unitOfWork.ReviewRepository.Find(r => r.EventID == eventId);
            return Ok(reviews);
        }

        // DELETE: api/Event/DeleteUser/5
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Event/DeleteEvent/5
        [HttpDelete("DeleteEvent/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var evnt = await _unitOfWork.EventRepository.GetByIdAsync(id);
            if (evnt == null)
            {
                return NotFound();
            }

            _unitOfWork.EventRepository.Delete(evnt);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}

