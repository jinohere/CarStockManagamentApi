using System.ComponentModel.DataAnnotations;

namespace CarStockManagamentApi.Models
{
    /// <summary>
    /// Represents a car in the inventory.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Unique identifier for the car.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier for the dealer who owns the car.
        /// </summary>
        [Required(ErrorMessage = "Dealer ID is required.")]
        public string DealerId { get; set; }

        /// <summary>
        /// The make or brand of the car (e.g., Audi, BMW).
        /// </summary>
        [Required(ErrorMessage = "Car make is required.")]
        [StringLength(50, ErrorMessage = "Car make cannot exceed 50 characters.")]
        public string Make { get; set; }

        /// <summary>
        /// The model of the car (e.g., A4, X5).
        /// </summary>
        [Required(ErrorMessage = "Car model is required.")]
        [StringLength(50, ErrorMessage = "Car model cannot exceed 50 characters.")]
        public string Model { get; set; }

        /// <summary>
        /// The manufacturing year of the car.
        /// Must be between 1986 and the current year.
        /// </summary>
        [Range(1986, int.MaxValue, ErrorMessage = "Car year must be between 1986 and the current year.")]
        public int Year { get; set; }

        /// <summary>
        /// The number of cars available in stock.
        /// Must be a non-negative number.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative number.")]
        public int StockQuantity { get; set; }
    }
}
