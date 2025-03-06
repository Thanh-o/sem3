using Microsoft.AspNetCore.Mvc;
using ATMManagementApplication.Models;
using ATMManagementApplication.Data;
using System.Linq;

namespace ATMManagementApplication.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ATMContext _context;

        public AuthController(ATMContext context)
        {
            _context = context;
        }

[HttpPost("login")]
public IActionResult Login([FromBody] LoginRequest login)
{
    if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
    {
        return BadRequest(ModelState);
    }

    // Tìm khách hàng dựa trên email và mật khẩu
    var customer = _context.Customers
        .FirstOrDefault(c => c.Email == login.Email && c.Password == login.Password);

    // Nếu không tìm thấy khách hàng, trả về lỗi
    if (customer == null)
    {
        return Unauthorized(new { Message = "Invalid credentials" });
    }

    // Trả về thông tin khách hàng
    return Ok(new { Message = "Login successful", CustomerId = customer.CustomerId });
}



        // Đăng ký người dùng
        [HttpPost("register")]
        public IActionResult Register([FromBody] Customer register)
        {
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.Email == register.Email);
            if (existingCustomer != null) return BadRequest("Email đã được sử dụng");

            var newCustomer = new Customer
            {
                Name = register.Name,
                Email = register.Email,
                Password = register.Password,
                Balance = 0
            };
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();

            return Ok(new { Message = "Registration successful" });
        }

        // Thay đổi mật khẩu
        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var customer = _context.Customers.Find(request.CustomerId);
            if (customer == null) return NotFound("Customer not found");

            // Kiểm tra mật khẩu cũ
            if (customer.Password != request.OldPassword) return BadRequest("Old password is incorrect");

            // Cập nhật mật khẩu mới
            customer.Password = request.NewPassword;
            _context.SaveChanges();

            return Ok(new { Message = "Password changed successfully" });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordRequest
    {
        public int CustomerId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
