using System;
using System.Collections.Generic;
using System.Linq;

namespace VMS.WebApp.Models
{
    public class BookingSummaryViewModel
    {
        public int BookingId { get; set; }
        public string EventName { get; set; } = "";
        public DateTime BookingDate { get; set; }
        public TimeSpan? BookingTime { get; set; }
        public decimal TotalPrice { get; set; }
        public string BookingStatus { get; set; } = "";
    }

    public class AccountViewModel
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";

        public List<BookingSummaryViewModel> Bookings { get; set; } = new();

        // Display Name
        public string DisplayName =>
            string.Join(" ",
                new[] { FirstName, LastName }
                    .Where(s => !string.IsNullOrWhiteSpace(s)))
            .Trim();

        // 👉 Add this (fixes your error)
        public string Initials =>
            (string.IsNullOrWhiteSpace(FirstName) ? "U" : FirstName[0].ToString().ToUpper()) +
            (string.IsNullOrWhiteSpace(LastName) ? "N" : LastName[0].ToString().ToUpper());
    }
}
