namespace VMS.WebApp.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int VisitorID { get; set; }
        public int EventID { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalPrice { get; set; }

        // Navigation properties - creates relationships
        public Visitor Visitor { get; set; } = null!;
        public Event Event { get; set; } = null!;
    }
}
