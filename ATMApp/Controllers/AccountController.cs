using Microsoft.AspNetCore.Mvc;
using ATMapp.Data;
using ATMapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ATMapp.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ATMContext _context;

        public AccountController(ATMContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _context.Accounts.Include(a => a.User).Include(a => a.Transactions).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Accounts
                                        .Include(a => a.User)
                                        .Include(a => a.Transactions)
                                        .FirstOrDefaultAsync(a => a.AccountId == id);
            if (account == null) return NotFound();
            return account;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            if (!Enum.IsDefined(typeof(AccountType), account.Type))
            {
                return BadRequest(new { error = "Invalid account type" });
            }

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAccount), new { id = account.AccountId }, account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, Account account)
        {
            if (id != account.AccountId) return BadRequest();

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Accounts.Any(e => e.AccountId == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return NotFound();

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return NoContent();
        }



        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] decimal amount)
        {
            // Tìm kiếm tài khoản dựa trên id
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound("Account not found.");

            // Cập nhật số dư tài khoản
            account.Balance += amount;

            // Tạo giao dịch mới
            var transaction = new Transaction
            {
                AccountId = id,
                Amount = amount,
                Timestamp = DateTime.Now,
                Description = "Deposit",
                IsSuccessful = true
            };

            // Thêm giao dịch vào database
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(account);
        }


        // Rút tiền từ tài khoản
        [HttpPost("{id}/withdraw")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] decimal amount)
        {
            if (amount <= 0) return BadRequest("Amount must be greater than zero.");

            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return NotFound("Account not found.");

            if (account.Balance < amount) return BadRequest("Insufficient balance.");

            account.Balance -= amount;

            var transaction = new Transaction
            {
                AccountId = account.AccountId,
                Amount = -amount,
                 Timestamp = DateTime.Now,
                IsSuccessful = true,
                Description = "Withdrawal"
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(new { account.Balance });
        }
    }
}
