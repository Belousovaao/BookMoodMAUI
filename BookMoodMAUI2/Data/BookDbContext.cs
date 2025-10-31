using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookMoodMAUI2.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.DateAdded).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
