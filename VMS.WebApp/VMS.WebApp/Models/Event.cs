namespace VMS.WebApp.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public int AvailableTickets { get; set; }
        public decimal TicketPrice { get; set; }

        // Navigation property
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
