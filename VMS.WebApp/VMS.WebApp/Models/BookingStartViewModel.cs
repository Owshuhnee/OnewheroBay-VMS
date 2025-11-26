using System;
using System.ComponentModel.DataAnnotations;

namespace VMS.WebApp.Models
{
    public class BookingStartViewModel
    {
        // Event info (display only on the left card)
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? EventImage { get; set; }
        public string? Schedule { get; set; }
        public decimal? TicketPrice { get; set; }
        public int? AvailableSlots { get; set; }

        // Booking form fields (user input on the right)
        [Required]
        [DataType(DataType.Date)]
        public DateTime PreferredDate { get; set; }

        [Required]
        public TimeSpan PreferredTime { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Guest count must be at least 1")]
        public int GuestCount { get; set; } = 1;

        public string? SpecialRequest { get; set; }

        public decimal TotalPrice =>
            (TicketPrice ?? 0) * GuestCount;
    }
}
