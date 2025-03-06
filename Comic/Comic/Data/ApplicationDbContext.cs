using Microsoft.EntityFrameworkCore;

namespace Comic.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Định nghĩa DbSet cho mỗi bảng trong cơ sở dữ liệu
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình các khóa chính và quan hệ giữa các bảng nếu cần thiết
            modelBuilder.Entity<ComicBook>()
                .HasKey(cb => cb.Id); // Giả định 'Id' là khóa chính của bảng ComicBook

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id); // Giả định 'Id' là khóa chính của bảng Customer

            modelBuilder.Entity<Rental>()
                .HasKey(r => r.Id); // Giả định 'Id' là khóa chính của bảng Rental

            modelBuilder.Entity<RentalDetail>()
                .HasKey(rd => rd.Id); // Giả định 'Id' là khóa chính của bảng RentalDetail

            // Thiết lập quan hệ giữa bảng Rentals và RentalDetails
            modelBuilder.Entity<RentalDetail>()
                .HasOne(rd => rd.Rental)
                .WithMany(r => r.RentalDetails)
                .HasForeignKey(rd => rd.RentalId);

            // Thiết lập quan hệ giữa bảng ComicBook và RentalDetails
            modelBuilder.Entity<RentalDetail>()
                .HasOne(rd => rd.ComicBook)
                .WithMany(cb => cb.RentalDetails)
                .HasForeignKey(rd => rd.ComicBookId);
        }
    }
}
