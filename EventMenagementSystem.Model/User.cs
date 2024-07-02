using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Model
{
    public enum UserType
    {
        Admin,
        Organizer,
        User
    }

    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public string? JwtToken { get; set; } = null!;

        [Required]
        public UserType UserType { get; set; }

        // Veza sa događajima na koje je korisnik prijavljen
        public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();

        // Veza sa događajima koje organizuje (samo za korisnike koji su organizatori)
        public ICollection<Event> OrganizedEvents { get; set; } = new List<Event>();

        // Veza sa recenzijama koje je korisnik ostavio
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}





