using Microsoft.AspNetCore.Mvc;
using Comic.Models;
using Microsoft.EntityFrameworkCore;

namespace Comic.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports/Index
        public async Task<IActionResult> Index()
        {
            var report = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                .ThenInclude(rd => rd.ComicBook)
                .ToListAsync();

            return View(report);
        }

        // GET: Reports/ComicBooksRented
        public async Task<IActionResult> ComicBooksRented()
        {
            var comicBooks = await _context.ComicBooks
                .Include(cb => cb.RentalDetails)
                .ThenInclude(rd => rd.Rental)
                .ToListAsync();

            var rentedBooks = comicBooks.Where(cb => cb.RentalDetails.Any(rd => rd.Rental.ReturnDate > DateTime.Now));
            return View(rentedBooks);
        }
    }
}
