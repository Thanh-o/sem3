using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comic.Models
{
    public class RentalDetail
    {
        public int Id { get; set; } // Khóa chính

        [Required]
        public int RentalId { get; set; } // Khóa ngoại từ bảng Rentals

        [Required]
        public int ComicBookId { get; set; } // Khóa ngoại từ bảng ComicBooks

        [Required]
        public int Quantity { get; set; } // Số lượng sách được thuê

        [Required]
        public decimal PricePerDay { get; set; } // Giá thuê mỗi ngày

        // Thiết lập quan hệ với bảng Rentals
        public Rental Rental { get; set; }

        // Thiết lập quan hệ với bảng ComicBooks
        public ComicBook ComicBook { get; set; }
    }
}
