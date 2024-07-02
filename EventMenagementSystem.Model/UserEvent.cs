using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Model
{
    // Veza između korisnika i događaja kojima su prijavljeni
    public class UserEvent
    {
        [Key, Column(Order = 0)]
        public int UserID { get; set; }
        public User User { get; set; } = null!;

        [Key, Column(Order = 1)]
        public int EventID { get; set; }
        public Event Event { get; set; } = null!;

        // Novi atribut za broj rezervisanih mesta
        [Required]
        public int SpotsReserved { get; set; }
    }
}




