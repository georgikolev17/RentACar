namespace RentACar.Models
{
    public class CarRequest
    {
        public CarRequest(string? userID, string? carID, DateOnly startDate, DateOnly endDate)
        {
            UserID = userID;
            CarID = carID;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string? UserID;

        public string? CarID;

        public DateOnly StartDate;
        
        public DateOnly EndDate;
    }
}
