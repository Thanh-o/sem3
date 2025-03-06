using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comic.Models
{
    public class ComicBook
    {
        [Key]
        public int Id { get; set; }  // Khóa chính

        [Required]
        [StringLength(100)]
        public string Title { get; set; }  // Tên truyện

        [StringLength(500)]
        public string Description { get; set; }  // Mô tả truyện

        [Required]
        public decimal PricePerDay { get; set; }  // Giá thuê mỗi ngày

        [Required]
        public int StockQuantity { get; set; }  // Số lượng sách có sẵn

        public bool IsAvailable { get; set; }  // Trạng thái có sẵn hay không

        public DateTime DateAdded { get; set; }  // Ngày thêm vào hệ thống

        [StringLength(100)]
        public string Author { get; set; }  // Tên tác giả của truyện

        // Quan hệ với RentalDetails (nhiều thuê mượn có thể liên quan tới 1 ComicBook)
        public ICollection<RentalDetail> RentalDetails { get; set; }
    }
}
