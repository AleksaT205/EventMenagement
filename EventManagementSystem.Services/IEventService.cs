using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementSystem.Model;

namespace EventManagementSystem.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEvents();
        Task<Event> GetEventById(int id);
        Task<Event> CreateEvent(Event evnt);
        Task DeleteEvent(int id);
        Task UpdateEvent(Event evnt); 
    }
}

