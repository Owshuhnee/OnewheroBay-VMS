namespace VMS.WebApp.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int VisitorID { get; set; }
        public int EventID { get; set; }
        public DateTime BookingDate { get; set; }
        public int UserID { get; set; }  // ✅ ADD THIS - it's in database
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }  // ✅ ADD THIS BACK - it's in database

        // REMOVE NumberOfTickets - it doesn't exist in database
        // public int NumberOfTickets { get; set; }  ❌ Remove this

        // Navigation properties
        public Visitor Visitor { get; set; } = null!;
        public Event Event { get; set; } = null!;
    }
}