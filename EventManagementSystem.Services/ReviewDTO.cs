using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.Services
{
    public class ReviewDTO
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

}
