using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMS.WebApp.Models
{
    [Table("events")]
    public class Event
    {
        [Key]
        [Column("event_id")]
        public int EventId { get; set; }

        [Column("event_name")]
        public string EventName { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("event_image")]
        public string? EventImage { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("ticket_price")]
        public decimal? TicketPrice { get; set; }

        [Column("available_slots")]
        public int? AvailableSlots { get; set; }

        [Column("schedule")]
        public string? Schedule { get; set; }

        [Column("interest")]
        public string? Interest { get; set; }

        // OPTIONAL but useful for analytics – doesn’t change DB schema
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
