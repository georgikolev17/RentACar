using RentACar.Models;

namespace RentACar.Data
{
    public class CarServices
    {
        public ApplicationDbContext db;

        public CarServices(ApplicationDbContext context) {
            db = context;
        }
        public bool AddRequest(CarRequest _request)
        {
            if(!IsOverlap(_request.StartDate, _request.EndDate)) {
                db.CarRequests.Add(_request);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task AddCar(Car car)
        {
            await db.Cars.AddAsync(car);
            await db.SaveChangesAsync();
        }

        public async Task RemoveCar(Car car)
        {
            await db.Cars.AddAsync(car);
            await db.SaveChangesAsync();
        }

        public List<CarRequest> CarsRequestedByUser(ApplicationUser user)
        {
            var userRequests = db.CarRequests.
                Where(r => r.UserId.Equals(user.Id))
                .ToList();
            return userRequests;
        }


        public List<ApplicationUser> UsersRequestingCar(Car car)
        {
            var carRequestsForCarID = db.CarRequests.Where(r => r.CarId.Equals(car.Id)).ToList();
            var users = carRequestsForCarID.Select(r => r.User).ToList();

            return users;
        }

        public bool IsOverlap(DateTime newStartDate, DateTime newEndDate)
        {
            // Fetch existing rental requests that overlap with the new request
            var overlappingRequests = db.CarRequests
                .Where(r =>
                    (newStartDate >= r.StartDate && newStartDate <= r.EndDate) ||
                    (newEndDate >= r.StartDate && newEndDate <= r.EndDate) ||
                    (newStartDate <= r.StartDate && newEndDate >= r.EndDate))
                .ToList();

            // If there are any overlapping requests, return true; otherwise, return false
            return overlappingRequests.Any();
        }

        public IEnumerable<Car> GetAllCars()
        {
            return this.db.Cars;
        }
    }
}
