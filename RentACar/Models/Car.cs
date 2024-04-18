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

        [Display(Name = "Number of passengers")]
        public int NumPassengers { get; set; }

        public string? Description { get; set; }

        [Display(Name = "Price/day")]
        public int PricePerDay { get; set; }
    }
}
