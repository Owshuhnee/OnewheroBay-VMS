namespace VMS.WebApp.Models
{
    public class Booking
    {
        public int BookingID { get; set; }

        public int UserID { get; set; }

        public int EventID { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "pending";



        // Navigation properties - creates relationships

        public Visitor Visitor { get; set; } = null!;

        public Event Event { get; set; } = null!;
    }
}

