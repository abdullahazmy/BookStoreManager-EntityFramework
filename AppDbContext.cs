using BookStoreManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManager
{
    internal class AppDbContext : DbContext
    {
        private const string Connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreManagement;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(Connection);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Author).IsRequired();
                entity.Property(e => e.PublishDate).HasDefaultValueSql("GETDATE()");
            });



            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);


            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(150);
            });

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
