using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventManagementSystem.Model;
using EventMenagementSystem.EntityFramework.Repositories;

namespace EventManagementSystem.Repositories
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        
    }
}