using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class CarRequest
    {
        public CarRequest(string userID, string carID, DateTime startDate, DateTime endDate)
        {
            this.UserId = userID;
            this.CarId = carID;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
        public CarRequest()
        {
        }

        [Required]
        public string UserId;
        public ApplicationUser User { get; set; }

        [Required]
        public string CarId { get; set; }
        public Car Car { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
