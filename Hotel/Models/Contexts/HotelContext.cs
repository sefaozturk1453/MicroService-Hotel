using Microsoft.EntityFrameworkCore;
using System;

namespace Hotel.Models.Contexts
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }

        public DbSet<Official> Officials { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<ReportDetail> ReportDetails { get; set; }
        public DbSet<ReportRequest> ReportRequests { get; set; }
    }
}
