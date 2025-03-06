using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comic.Models
{
    public class Rental
    {
        public int Id { get; set; } // Khóa chính

        [Required]
        public DateTime RentalDate { get; set; } // Ngày thuê

        [Required]
        public DateTime ReturnDate { get; set; } // Ngày trả

        [Required]
        public int CustomerId { get; set; } // Khóa ngoại từ bảng Customers

        // Quan hệ với khách hàng
        public Customer Customer { get; set; }

        // Trạng thái của thuê mượn (e.g., "Pending", "Completed", "Cancelled")
        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Trạng thái của việc thuê (Pending, Completed, Cancelled)

        // Một lần thuê có thể có nhiều chi tiết thuê sách
        public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>(); 
    }
}
