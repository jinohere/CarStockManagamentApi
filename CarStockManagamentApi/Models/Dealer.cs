using System.ComponentModel.DataAnnotations;

namespace CarStockManagamentApi.Models
{
    /// <summary>
    /// Represents a dealer in the system.
    /// </summary>
    public class Dealer
    {
        /// <summary>
        /// Unique identifier for the dealer.
        /// </summary>
        [Required(ErrorMessage = "Dealer ID is required.")]
        [StringLength(50, ErrorMessage = "Dealer ID cannot exceed 50 characters.")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the dealer.
        /// </summary>
        [Required(ErrorMessage = "Dealer name is required.")]
        [StringLength(100, ErrorMessage = "Dealer name cannot exceed 100 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Location of the dealer.
        /// </summary>
        [Required(ErrorMessage = "Dealer location is required.")]
        [StringLength(150, ErrorMessage = "Dealer location cannot exceed 150 characters.")]
        public string Location { get; set; }
    }
}
