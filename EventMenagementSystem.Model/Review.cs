using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementSystem.Model
{
    public class Review
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Rating { get; set; } // Ocena od 1 do 5

        public string Comment { get; set; } = null!;// Opcioni komentar

        // Veza sa korisnikom koji je ostavio recenziju
        public int UserID { get; set; }
        //[ForeignKey(nameof(UserID))]
        public User User { get; set; } = null!;

        // Veza sa događajem na koji se odnosi recenzija
        public int EventID { get; set; }
        public Event Event { get; set; } = null!;
    }
}

