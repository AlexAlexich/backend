using GoalFinalStage.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinalStage.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options) { }  

        public DbSet<User> Users { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemUser> ItemUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();

            modelBuilder.Entity<User>().HasMany(x=>x.Items).WithMany(x=>x.Users);

            modelBuilder.Entity<ItemUser>().HasKey(t => new { t.UserId, t.ItemId });

        }
    }
}
