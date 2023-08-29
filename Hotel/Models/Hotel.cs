using System;
using System.Collections.Generic;

namespace Hotel.Models
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
