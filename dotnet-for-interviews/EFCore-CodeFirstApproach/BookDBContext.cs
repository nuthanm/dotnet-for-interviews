using Microsoft.EntityFrameworkCore;

namespace EFCore_CodeFirstApproach
{
    public class BookDBContext : DbContext
    {
        public BookDBContext(DbContextOptions<BookDBContext> options) : base(options)
        {
        }

        public BookDBContext()
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KANINI-LTP-283;Database=BookDB;User Id=sa;Password=Admin@12345;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(b => b.Id); // Primary Key by convention
            modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Review>().HasKey(r => r.Id); // Primary Key by convention
            modelBuilder.Entity<Review>().Property(r => r.ReviewerName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Review>().Property(r => r.Comment).HasMaxLength(500);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId);
        }
    }
}
