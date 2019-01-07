

using Microsoft.EntityFrameworkCore;

namespace VenuesApi.Models
{
    public class VenuesDbContext : DbContext
    {
        public VenuesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Venue> Venues{set; get;}
        public DbSet<Customer> Customers {set; get;}
        public DbSet<Reservation> Reservations {set; get;}
    }
}