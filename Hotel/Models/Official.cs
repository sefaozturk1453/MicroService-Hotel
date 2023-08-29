using System;
using System.Collections.Generic;

namespace Hotel.Models
{
    public class Official
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
