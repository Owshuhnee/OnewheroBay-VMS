namespace VMS.WebApp.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int VisitorID { get; set; }
        public int EventID { get; set; }
        public DateTime BookingDate { get; set; }
        public int UserID { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }

        // Navigation properties
        public Visitor Visitor { get; set; } = null!;
        public Event Event { get; set; } = null!;
        public User User { get; set; } = null!;  // ← ADD THIS LINE
    }
}