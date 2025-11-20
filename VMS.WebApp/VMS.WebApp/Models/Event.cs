using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMS.WebApp.Models
{
    [Table("events")]
    public class Event
    {
        [Key]
        [Column("event_id")]
        public int EventID { get; set; }

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

        //"Monday to Friday", "Any day 9am–5pm"
        [Column("schedule")]
        public string? Schedule { get; set; }

        // All, Tours, Workshop, Nature, Special
        [Column("interest")] 
        public string? Interest { get; set; }
    }
}
