using Microsoft.EntityFrameworkCore;
using RPGTgBot.Infrastructure.Entities;


namespace RPGTgBot.Infrastructure.DataBaseContext
{
    public class RPGDbContext : DbContext
    {
        public RPGDbContext(DbContextOptions<RPGDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);
                e.Property(u => u.Name).IsRequired();
                e.Property(u => u.RegistrationDate).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
