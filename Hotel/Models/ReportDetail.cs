using System;

namespace Hotel.Models
{
    public class ReportDetail
    {
        public Guid Id { get; set; }
        public Guid ReportRequestId { get; set; }
        public ReportRequest ReportRequest { get; set; }
        public string Location { get; set; }
        public int HotelCount { get; set; }
        public int NumberCount { get; set; }
    }
}
