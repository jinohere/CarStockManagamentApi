using CarStockManagamentApi.Models;
using System.ComponentModel.DataAnnotations;

namespace CarStockManagamentApi.DTO
{
    public class CarDto
    {
        /// <summary>
        /// The make or brand of the car (e.g., Audi, BMW).
        /// This field is required.
        /// </summary>
        [Required(ErrorMessage = "Car make is required.")]
        [StringLength(50, ErrorMessage = "Car make cannot exceed 50 characters.")]
        public string Make { get; set; }

        /// <summary>
        /// The model of the car (e.g., A4, X5).
        /// This field is required.
        /// </summary>
        [Required(ErrorMessage = "Car model is required.")]
        [StringLength(50, ErrorMessage = "Car model cannot exceed 50 characters.")]
        public string Model { get; set; }

        /// <summary>
        /// The manufacturing year of the car.
        /// Must be between 1986 and 2024.
        /// </summary>
        [Range(1986, 2024, ErrorMessage = "Car year must be between 1986 and 2024.")]
        public int Year { get; set; }

        /// <summary>
        /// Converts the DTO to a Car model object with provided dealer ID, car ID, and stock quantity.
        /// </summary>
        /// <param name="dealerId">The ID of the dealer to which the car belongs.</param>
        /// <param name="carId">The unique ID for the car.</param>
        /// <param name="stockQuantity">The current stock quantity for the car.</param>
        /// <returns>A Car model object.</returns>
        public Car ToModel(string dealerId, int carId, int stockQuantity)
        {
            return new Car
            {
                Id = carId,
                Make = Make,
                Model = Model,
                Year = Year,
                DealerId = dealerId,
                StockQuantity = stockQuantity
            };
        }
    }
}
