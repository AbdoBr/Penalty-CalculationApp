using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenaltyCalculationApp.Data;
using PenaltyCalculationApp.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PenaltyCalculationApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookBorrowingsController : Controller
    {
        private readonly PenaltyCalculatorDbContext _context;

        public BookBorrowingsController(PenaltyCalculatorDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateBorrowingRecord")]
        public async Task<ActionResult> CreateBorrowingRecord(BookBorrowingVM bookBorrowingVm)
        {
            try
            {
                var country = await _context.Countries.SingleOrDefaultAsync(c => c.Id == bookBorrowingVm.CountryId);

                if (country == null)
                {
                    return NotFound("Country not found");
                }
                
                var bookBorrowing = new BookBorrowing
                {
                    BookTitle = bookBorrowingVm.BookTitle,
                    BorrowerName = bookBorrowingVm.BorrowerName,
                    BorrowingDate = DateTime.UtcNow,
                    DueDate = bookBorrowingVm.DueDate,
                    CountryId = bookBorrowingVm.CountryId,
                };

                _context.BookBorrowings.Add(bookBorrowing);
                _context.SaveChanges();
                return Ok(bookBorrowing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // retrieve the weekend configuration for the selected country
        private string GetWeekendConfigurationByCountry(int countryId)
        {
            var country = _context.Countries.FirstOrDefault(c => c.Id == countryId);
            return country != null ? country.WeekendConfiguration : "Saturday,Sunday"; // Default weekend configuration
        }

        // Method to calculate penalty based on days overdue and country's weekend configuration
        private decimal CalculatePenalty(int daysOverdue, int countryId)
        {
            var weekendConfiguration = GetWeekendConfigurationByCountry(countryId);
            var weekendDays = weekendConfiguration.Split(',');
            var overdueWeekdays = 0;

            // Calculate the number of weekdays excluding weekends
            for (var i = 0; i <= daysOverdue; i++)
            {
                var currentDay = DateTime.Now.AddDays(-i);
                if (!weekendDays.Contains(currentDay.DayOfWeek.ToString()))
                {
                    overdueWeekdays++;
                }
            }

            // Calculate penalty based on the number of overdue weekdays and a specific penalty rate
            var penaltyRate = 5; // penalty rate of 5 USD per overdue weekday
            var penalty = overdueWeekdays * penaltyRate;

            return penalty;
        }

        // CalculatePenalty method
        [HttpGet]
        [Route("CalculatePenalty")]
        public async Task<ActionResult> GetCalculatePenalty(DateTime BookCheckOutDate, DateTime BookCheckReturnDate, int countryId)
        {
            try
            {
                var bookBorrowing = await _context.BookBorrowings
                                            .Where(b => b.CountryId == countryId &&
                                                        b.DueDate >= BookCheckOutDate &&
                                                        b.DueDate <=BookCheckReturnDate)
                                            .ToListAsync();

                if (bookBorrowing == null)
                {
                    return NotFound("Book borrowing record not found");
                }

                decimal penalty = 0;
                // Calculate the number of days overdue
                foreach (var book in bookBorrowing)
                {


                    var currentDate = DateTime.Now;
                    var daysOverdue = (currentDate - book.DueDate).Days;

                    // Calculate the penalty based on the number of days overdue and the country's weekend configuration
                    penalty+= CalculatePenalty(daysOverdue, countryId);
                }
                return Ok(penalty);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Get All Countries
        [HttpGet]
        [Route("GetAllCountriess")]
        public async Task<IActionResult> GetAllCountriess()
        {
            try
            {
                var employees = await _context.Countries.ToListAsync();

            return Ok(employees);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

    }

}
