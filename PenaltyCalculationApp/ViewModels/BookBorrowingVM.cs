
namespace PenaltyCalculationApp.Model;

	public class BookBorrowingVM
{
    public string BookTitle { get; set; }
    public string BorrowerName { get; set; }
    public DateTime DueDate { get; set; }
    public int CountryId { get; set; }
}

