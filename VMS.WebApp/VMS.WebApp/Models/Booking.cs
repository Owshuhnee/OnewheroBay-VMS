using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMS.WebApp.Models
{
    [Table("bookings")]
    public class Booking
    {
        [Key]
        [Column("booking_id")]
        public int BookingId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("event_id")]
        public int EventId { get; set; }

        // Date the booking was created/requested (maps to 'booking_date' date column)
        [Column("booking_date")]
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        // User's chosen time (maps to 'booking_time' time column)
        [Column("booking_time")]
        public TimeSpan BookingTime { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        public string? Status { get; set; }

        [Column("guest_count")]
        public int GuestCount { get; set; }

        [Column("special_request")]
        public string? SpecialRequest { get; set; }

        [Column("booking_status")]
        public string BookingStatus { get; set; } = "pending";

        // Navigation properties
        public User User { get; set; } = null!;
        public Event Event { get; set; } = null!;
    }
}
