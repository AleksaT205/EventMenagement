using EventManagementSystem.Model;
using EventMenagementSystem.EntityFramework.Repositories;
using System.Threading.Tasks;

namespace EventManagementSystem.Repositories
{
    public interface IEventRepository : IBaseRepository<Event>
    {
    }
}
