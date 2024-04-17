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

        public void AddCar(Car car)
        {
            db.Cars.Add(car);
            db.SaveChanges();
        }

        public void RemoveCar(Car car)
        {
            db.Cars.Remove(car);
            db.SaveChanges();
        }

        public List<CarRequest> CarsRequestedByUser(ApplicationUser user)
        {
            var userRequests = db.CarRequests.
                Where(r => r.UserID.Equals(user.Id))
                .ToList();
            return userRequests;
        }

        public List<ApplicationUser> UsersRequestingCar(Car car)
        {
            var carRequestsForCarID = db.CarRequests.Where(r => r.CarID.Equals(car.CarUUID)).ToList();
            var UserIds = carRequestsForCarID.Select(r => r.UserID).ToList();
        }

        public bool IsOverlap(DateOnly newStartDate, DateOnly newEndDate)
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
    }
}
