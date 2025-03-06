using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comic.Models
{
    public class Customer
    {
        public int Id { get; set; } // Khóa chính

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } // Họ và tên khách hàng

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } // Số điện thoại

        [Required]
        public DateTime RegisterDate { get; set; } // Ngày đăng ký

        // Quan hệ một khách hàng có thể có nhiều lần thuê sách
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>(); 
    }
}
