namespace MovieRentalSystem.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; } = true;
}
