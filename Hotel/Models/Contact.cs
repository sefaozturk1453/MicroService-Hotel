using System;

namespace Hotel.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public InfoType Type { get; set; }
        public string Info { get; set; }
    }

    public enum InfoType
    {
        Phone,
        EMail,
        Location
    }
}
