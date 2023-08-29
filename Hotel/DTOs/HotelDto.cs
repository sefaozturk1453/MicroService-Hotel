using Hotel.Models;
using System;
using System.Collections.Generic;

namespace Hotel.DTOs
{
    public class HotelDto
    {
        public string Name { get; set; }
    }

    public class HotelContactDto
    {
        public Guid Id { get; set; }
        public byte Type { get; set; }
        public string Info { get; set; }
    }

    public class HotelContactResDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
    }

    public class OfficelResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
    }

    public class HotelContactResponseDto : HotelDto
    {
        public Guid Id { get; set; }
        public List<HotelContactResDto> Contacts { get; set; } = new List<HotelContactResDto>();
    }

    public class ReportRequestResponseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
