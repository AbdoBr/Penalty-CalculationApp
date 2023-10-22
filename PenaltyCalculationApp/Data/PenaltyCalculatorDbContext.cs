using System;
using Microsoft.EntityFrameworkCore;
using PenaltyCalculationApp.Model;

namespace PenaltyCalculationApp.Data
{
    public class PenaltyCalculatorDbContext : DbContext
    {
        public PenaltyCalculatorDbContext(DbContextOptions<PenaltyCalculatorDbContext> options) : base(options)
        {

        }

        public DbSet<BookBorrowing> BookBorrowings { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}

