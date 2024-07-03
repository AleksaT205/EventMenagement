<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagementSystem.Model;
using EventManagementSystem.Services;
using EventManagementSystem.UnitOfWork;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IReviewService _reviewService;
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;

    public EventController(IEventService eventService, IReviewService reviewService, IUserService userService, IUnitOfWork unitOfWork, AuthorizationController authorizationController)
    {
        _eventService = eventService;
        _reviewService = reviewService;
        _userService = userService;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("GetEvents")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
    {
        var events = await _eventService.GetAllEvents();
        return Ok(events);
    }

    [HttpPost("ReserveEvent")]
    public async Task<IActionResult> ReserveEvent(int userId, int eventId, int spotsToReserve)
    {
        var success = await _userService.ReserveEvent(userId, eventId, spotsToReserve);
        if (!success)
            return BadRequest("Reservation failed.");

        return Ok("Reservation successful.");
    }

    [HttpDelete("CancelReservation")]
    public async Task<IActionResult> CancelReservation(int userId, int eventId)
    {
        var success = await _userService.CancelReservation(userId, eventId);
        if (!success)
            return BadRequest("Canceling reservation failed.");

        return Ok("Reservation canceled.");
    }

    [HttpGet("GetUserReservations/{userId}")]
    public async Task<IActionResult> GetUserReservations(int userId)
    {
        try
        {
            var reservations = await _userService.GetUserReservations(userId);
            return Ok(reservations);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Interna serverska greška: {ex.Message}");
        }
    }


    [HttpPost("AddEvent")]
    public async Task<ActionResult<Event>> AddEvent([FromBody] EventDto eventDto)
    {
        try
        {
            // Provera validnosti podataka događaja
            if (string.IsNullOrWhiteSpace(eventDto.Name) ||
                eventDto.Date == DateTime.MinValue ||
                string.IsNullOrWhiteSpace(eventDto.Location) ||
                eventDto.Capacity <= 0)
            {
                return BadRequest("Invalid event data.");
            }

            // Kreiranje novog događaja
            var evnt = new Event
            {
                Name = eventDto.Name,
                Date = eventDto.Date,
                Location = eventDto.Location,
                Description = eventDto.Description,
                Capacity = eventDto.Capacity,
                OrganizerID = eventDto.OrganizerID // Dodajte ako je potrebno dodeljivanje organizatora
            };

            // Kreiranje događaja putem servisa
            var createdEvent = await _eventService.CreateEvent(evnt);

            return Ok(createdEvent);
        }
        catch (DbUpdateException dbEx)
        {
            // Loguj unutrašnji izuzetak
            Console.WriteLine($"Database error: {dbEx.Message}. Inner exception: {dbEx.InnerException?.Message}");

            // Vrati 500 status i detalje greške
            return StatusCode(500, $"Database error: {dbEx.Message}. Inner exception: {dbEx.InnerException?.Message}");
        }

    }

    [HttpPost("AddReview")]
    public async Task<IActionResult> AddReview([FromBody] ReviewDTO reviewDto)
    {
        var review = await _reviewService.AddReview(reviewDto);

        if (review == null)
        {
            return BadRequest("Unable to add review. User or event not found.");
        }

        return Ok(review);
    }

    [HttpDelete("DeleteReview/{userId}/{eventId}")]
    public async Task<IActionResult> DeleteReview(int userId, int eventId)
    {
        await _reviewService.DeleteReview(userId, eventId);
        return NoContent();
    }

    [HttpGet("GetReview")]
    public async Task<ActionResult<Review>> GetReview(int userId, int eventId)
    {
        var review = await _reviewService.GetReview(userId, eventId);
        if (review == null)
        {
            return NotFound();
        }
        return Ok(review);
    }

    [HttpGet("GetEventReviews/{eventId}")]
    public async Task<ActionResult<IEnumerable<Review>>> GetEventReviews(int eventId)
    {
        var reviews = await _reviewService.GetEventReviews(eventId);
        return Ok(reviews);
    }

    [HttpDelete("DeleteEvent/{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        await _eventService.DeleteEvent(id);
        return NoContent();
    }

    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);
        return NoContent();
=======
﻿using Microsoft.AspNetCore.Mvc;
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
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
    }
}

