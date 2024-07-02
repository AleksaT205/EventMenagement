using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventManagementSystem.Model;
using EventManagementSystem.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _unitOfWork.UserRepository.GetByUsernameAsync(username);
        }

        public async Task<User> CreateUser(User user)
        {
            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUser(User user)
        {
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user != null)
            {
                _unitOfWork.UserRepository.Delete(user);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<List<Event>> GetUserReservations(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetUserWithEventsAsync(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with ID {userId} not found.");
            }

            return user.UserEvents.Select(ue => ue.Event).ToList();
        }


        public async Task<bool> ReserveEvent(int userId, int eventId, int spotsToReserve)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new ApplicationException($"User with ID {userId} not found.");
                }

                var evnt = await _unitOfWork.EventRepository.GetByIdAsync(eventId);
                if (evnt == null)
                {
                    throw new ApplicationException($"Event with ID {eventId} not found.");
                }

                // Check if there are enough spots available
                if (evnt.Capacity < spotsToReserve || spotsToReserve <= 0)
                {
                    throw new ApplicationException($"Not enough spots available to reserve {spotsToReserve} spots.");
                }

                // Check if the user already has a reservation for this event
                var existingReservation = user.UserEvents.FirstOrDefault(ue => ue.EventID == eventId);
                if (existingReservation != null)
                {
                    // If user already has a reservation, update the spots reserved
                    evnt.Capacity -= spotsToReserve; // oduzme od kapaciteta i taj novi broj
                    existingReservation.SpotsReserved += spotsToReserve; //doda taj broj u rezervacije user vent tabele
                }
                else
                {
                    // ako nema rezervacija za taj dogadjaj, oduzima od ukupnog broja slobodnih mesta i pravi novu rezrvaciju i upisuje podatke
                    // If no existing reservation, add a new reservation
                    evnt.Capacity -= spotsToReserve;
                    user.UserEvents.Add(new UserEvent { UserID = userId, EventID = eventId, SpotsReserved = spotsToReserve });
                }

                await _unitOfWork.SaveChangesAsync();

                return true; // Return true on successful reservation
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                throw new ApplicationException("Error reserving event.", ex);
            }
        }

        public async Task<bool> CancelReservation(int userId, int eventId)
        {
            try
            {
                Console.WriteLine($"Starting cancellation process for User ID = {userId}, Event ID = {eventId}");

                var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    Console.WriteLine($"User with ID {userId} not found.");
                    throw new ApplicationException($"User with ID {userId} not found.");
                }

                var userEvent = await _unitOfWork.UserEventRepository.GetByConditionAsync(ue => ue.UserID == userId && ue.EventID == eventId);
                if (userEvent == null)
                {
                    Console.WriteLine($"User {userId} does not have a reservation for Event {eventId}.");
                    throw new ApplicationException($"User {userId} does not have a reservation for Event {eventId}.");
                }

                var evnt = await _unitOfWork.EventRepository.GetByIdAsync(eventId);
                if (evnt == null)
                {
                    Console.WriteLine($"Event with ID {eventId} not found.");
                    throw new ApplicationException($"Event with ID {eventId} not found.");
                }

                Console.WriteLine($"Cancelling reservation: User ID = {userId}, Event ID = {eventId}, Spots Reserved = {userEvent.SpotsReserved}");

                evnt.Capacity += userEvent.SpotsReserved;
                _unitOfWork.UserEventRepository.Delete(userEvent);
                await _unitOfWork.SaveChangesAsync();

                Console.WriteLine("Cancellation successful.");
                return true;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Application error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw new ApplicationException("Error canceling reservation.", ex);
            }
        }

    }
}


