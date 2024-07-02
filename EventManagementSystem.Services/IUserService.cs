using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementSystem.Model;

namespace EventManagementSystem.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task<User> CreateUser(User user);
        Task DeleteUser(int id);
        Task UpdateUser(User user);

        Task<bool> ReserveEvent(int userId, int eventId, int spotsToReserve);
        Task<bool> CancelReservation(int userId, int eventId);
        Task<List<Event>> GetUserReservations(int userId);
    }
}



