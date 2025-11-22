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

        [Column("booking_date")]
        public DateTime BookingDate { get; set; }

        [Column("booking_time")]
        public TimeSpan BookingTime { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        [Column("booking_status")]
        public string BookingStatus { get; set; } = "pending";

        [Column("special_request")]
        public string? SpecialRequest { get; set; }

        [Column("guest_count")]
        public int GuestCount { get; set; }


        // navigation properties (no extra columns)
        public User User { get; set; } = null!;
        public Event Event { get; set; } = null!;
    }
}
