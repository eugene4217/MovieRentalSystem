using System;

namespace MovieRentalSystem.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int ClientId { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime PlannedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; } 

        public double TotalCost { get; set; }
        public double Penalty { get; set; }
    }
}