using System;
using System.ComponentModel.DataAnnotations;

namespace PenaltyCalculationApp.Model
{
	public class BookBorrowing
	{
        [Key]
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string BorrowerName { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime DueDate { get; set; }
        public int CountryId { get; set; }
    }
}

