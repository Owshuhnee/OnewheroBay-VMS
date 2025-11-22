using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VMS.WebApp.Models
{
    public class RegisterViewModel
    {

        //Register Form
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public string? Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;

        // Structured interests that map to the event filters
        // Controlled values will be: "tours", "nature", "workshops", "special"
        public List<string> SelectedInterests { get; set; } = new();
    }
}
