using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.API.Models
{
    public class DeliveryContext : DbContext
    {
        /// <summary>
        /// Sql Db connector
        /// </summary>
        /// <param name="options"></param>
        public DeliveryContext(DbContextOptions<DeliveryContext> options) :
            base(options)
        {
        }
   
        public DbSet<Product> Products{ get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryAction> DeliveryActions { get; set; }
    }
}
