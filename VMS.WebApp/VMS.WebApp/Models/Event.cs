namespace VMS.WebApp.Models
{
    public class Event
    {
        public int EventID { get; set; }

        public string EventName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public string? EventImage { get; set; } //File path or URL

        public List<EventSession> Sessions { get; set; } = new();
    }
}