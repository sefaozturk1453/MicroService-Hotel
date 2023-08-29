using System;

namespace Hotel.Models
{
    public class ReportRequest
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public StatusType Status { get; set; }
    }

    public enum StatusType
    {
        Preparing,
        Done
    }
}
