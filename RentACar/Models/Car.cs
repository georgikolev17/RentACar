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
            CarUUID = Guid.NewGuid().ToString();
        }

        public string CarUUID;

        public string? Make { get; set; }

        public string? Model { get; set; }

        public string? Year { get; set; }

        public int NumPassengers { get; set; }

        public string? Description { get; set; }

        public int PricePerDay { get; set; }
    }
}
