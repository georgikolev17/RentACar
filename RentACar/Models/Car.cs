using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class Car
    {
        public Car(string? make, string? model, string? year, int numPassengers, string? description, int pricePerDay)
        {
            Make = make;
            Model = model;
            Year = year;
            NumPassengers = numPassengers;
            Description = description;
            PricePerDay = pricePerDay;
            Id = Guid.NewGuid().ToString();
        }
        public Car()
        {
        }

        [Required]
        public string Id { get; set; }

        public string? Make { get; set; }

        public string? Model { get; set; }

        public string? Year { get; set; }

        public int NumPassengers { get; set; }

        public string? Description { get; set; }

        public int PricePerDay { get; set; }
    }
}
