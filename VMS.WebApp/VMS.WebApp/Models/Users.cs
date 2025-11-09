namespace VMS.WebApp.Models
{
    public class User
    {
        public int UserId { get; set; }         
        public string Role { get; set; } = "Visitor";
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!; 
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
