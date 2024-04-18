using RentACar.Data;
using RentACar.Models;

namespace RentACar.Seeding
{
    public class CarsSeeder : ISeeder
    {
        private const int seededCars = 10;
        private string[] makes = { "BMW", "Audi", "Mercedes", "Toyota", "Ford", "Peugeot", "Renault", "Opel", "Mazda", "Honda" };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            for (int i = 0; i < seededCars; i++)
            {
                var car = new Car(makes[i], "A", (2010+i).ToString(), 4, $"Very good {makes[i]} car", 20);
                await dbContext.Cars.AddAsync(car);
            }
        }
    }
}
