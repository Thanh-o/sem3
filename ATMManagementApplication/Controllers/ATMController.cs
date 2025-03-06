using Microsoft.AspNetCore.Mvc;
using ATMManagementApplication.Models;
using ATMManagementApplication.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ATMManagementApplication.Controllers
{
    [ApiController]
    [Route("api/atm")]
    public class ATMController : Controller
    {
        private readonly ATMContext _context;

        public ATMController(ATMContext context)
        {
            _context = context;
        }

        [HttpGet("Balance/{customerId}")]
        public async Task<IActionResult> GetBalance(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return NotFound("Customer not found");

            var balance = Math.Round(customer.Balance, 2);
            return Ok(new { Balance = balance });
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] WithdrawRequest request)
        {
            var customer = _context.Customers.Find(request.CustomerId);
            if (customer == null) return NotFound("Customer not found");

            if (customer.Balance < request.Amount) return BadRequest("Insufficient balance");

            customer.Balance -= request.Amount;

            var transaction = CreateTransaction(request.CustomerId, request.Amount, "Successful withdrawal");

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return Ok(new { Message = "Withdrawal successful", Balance = customer.Balance });
        }

        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] DepositRequest request)
        {
            var customer = _context.Customers.Find(request.CustomerId);
            if (customer == null) return NotFound("Customer not found");

            customer.Balance += request.Amount;

            var transaction = CreateTransaction(request.CustomerId, request.Amount, "Successful deposit");

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return Ok(new { Message = "Deposit successful", Balance = customer.Balance });
        }

        [HttpGet("transaction-history/{customerId}")]
        public IActionResult GetTransactionHistory(int customerId)
        {
            var transactions = _context.Transactions
                .Where(t => t.CustomerId == customerId)
                .OrderByDescending(t => t.Timestamp)
                .ToList();

            return Ok(transactions);
        }

        [HttpPost("transfer")]
        public IActionResult Transfer([FromBody] TransferRequest request)
        {
            var sender = _context.Customers.Find(request.SenderId);
            var receiver = _context.Customers.Find(request.ReceiverId);

            if (sender == null || receiver == null) return NotFound("Sender or Receiver not found");
            if (sender.Balance < request.Amount) return BadRequest("Insufficient balance");

            sender.Balance -= request.Amount;
            receiver.Balance += request.Amount;

            _context.Transactions.Add(CreateTransaction(request.SenderId, -request.Amount, $"Transfer to {receiver.Name}"));
            _context.Transactions.Add(CreateTransaction(request.ReceiverId, request.Amount, $"Transfer from {sender.Name}"));

            _context.SaveChanges();

            SendEmail(sender.Email, "Transfer Confirmation", $"You have transferred {request.Amount} to {receiver.Name}.");
            SendEmail(receiver.Email, "Money Received", $"You have received {request.Amount} from {sender.Name}.");

            return Ok(new { Message = "Transfer successful", SenderBalance = sender.Balance, ReceiverBalance = receiver.Balance });
        }

        private Transaction CreateTransaction(int customerId, decimal amount, string description)
        {
            return new Transaction
            {
                CustomerId = customerId,
                Amount = amount,
                Timestamp = DateTime.Now,
                IsSuccessful = true,
                Description = description
            };
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("hongphuc0835@gmail.com", "joia vkwu vppg pdvu"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("hongphuc0835@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);
            smtpClient.Send(mailMessage);
        }
    }

    public class WithdrawRequest
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
    }

    public class DepositRequest
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
    }

    public class TransferRequest
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal Amount { get; set; }
    }
}
