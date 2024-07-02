using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Model
{
    public class Event
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Required]
        public int Capacity { get; set; }

        // OrganizerID je ID korisnika koji organizuje događaj
        public int OrganizerID { get; set; }
        public User Organizer { get; set; } = null!;

        // Veza sa korisnicima koji su prijavljeni na događaj
        public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();

        // Veza sa recenzijama za ovaj događaj
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}




