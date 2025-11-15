using System;

namespace VMS.WebApp.Models
{
    public class EventSession
    {
        public int SessionID { get; set; }

        public int EventID { get; set; }
        public Event Event { get; set; } = null!;

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }

        public int Capacity { get; set; }
        public decimal Price { get; set; }

        public bool IsActive { get; set; }
    }
}
