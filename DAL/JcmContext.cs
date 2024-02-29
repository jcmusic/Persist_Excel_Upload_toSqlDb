using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL
{
    public class JcmContext : DbContext
    {
        public JcmContext(DbContextOptions<JcmContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasIndex(x => new { x.OrderNumber }).IsUnique();
        }
    }
}
