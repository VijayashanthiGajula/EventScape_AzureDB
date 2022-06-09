using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventScape.Models
{
    public class Events
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string EventName { get; set; } = null!;
        public DateTime? ShowStartDate { get; set; } = null!;
        public DateTime? ShowEndDate { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int? MaxCapacity { get; set; } = null!;
        public string? Description { get; set; } = null!;
        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }
        public string EventPosterName { get; set; }

       // public virtual ICollection<Booking>? Booking { get; set; } = null!;
       public virtual ICollection<UserQueries>? UserQueries { get; set; } = null!;
       // public virtual ICollection<NonUserQueries>? NonUserQueries { get; set; } = null!;

        public Events()
        {

        }
    }
}
