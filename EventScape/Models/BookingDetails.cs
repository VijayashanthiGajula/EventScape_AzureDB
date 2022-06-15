using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventScape.Models
{
    public class BookingDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookingID { get; set; }
        [ValidateNever]
        public Booking Booking { get; set; }

        [Required]
        public int EventId { get; set; }
        [ValidateNever]
        public virtual Events Event { get; set; }

        public int No_Of_Tickets { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal UnitPrice { get; set; }
    }
}
