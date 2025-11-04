namespace VMS.WebApp.Models
{
    public class Visitor
    {
        public int VisitorID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Interests { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }

        // Navigation property - links to Booking table
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
