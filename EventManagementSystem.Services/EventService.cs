using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementSystem.Model;
using EventManagementSystem.Repositories;
using EventManagementSystem.UnitOfWork;

namespace EventManagementSystem.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _unitOfWork.EventRepository.GetAllAsync();
        }

        public async Task<Event> GetEventById(int id)
        {
            return await _unitOfWork.EventRepository.GetByIdAsync(id);
        }

        public async Task<Event> CreateEvent(Event evnt)
        {
            _unitOfWork.EventRepository.Add(evnt);
            await _unitOfWork.SaveChangesAsync();
            return evnt;
        }

        public async Task UpdateEvent(Event evnt)
        {
            _unitOfWork.EventRepository.Update(evnt);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task DeleteEvent(int id)
        {
            var evnt = await _unitOfWork.EventRepository.GetByIdAsync(id);
            if (evnt != null)
            {
                _unitOfWork.EventRepository.Delete(evnt);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}

