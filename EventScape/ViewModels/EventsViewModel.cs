using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventScape.ViewModels
{
    public class EventsViewModel
    {
        public string EventName { get; set; } = null!;
        public DateTime? ShowStartDate { get; set; } = null!;
        public DateTime? ShowEndDate { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int? MaxCapacity { get; set; } = null!;
        public string? Description { get; set; } = null!;
       
        public decimal Price { get; set; }
       // public string EventPosterName { get; set; } = null!;

        [Required(ErrorMessage="Please upload Event Poster")]
        [Display(Name ="Event Posters")]
        [NotMapped]
        public IFormFile EventPosters { get; set; }
    }
}
