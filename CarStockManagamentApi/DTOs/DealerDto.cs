using System.ComponentModel.DataAnnotations;

namespace CarStockManagamentApi.Models
{
    /// <summary>
    /// Data transfer object (DTO) for Dealer.
    /// Used to capture and transfer dealer-related data between layers.
    /// </summary>
    public class DealerDto
    {
        /// <summary>
        /// The name of the dealer.
        /// This field is required.
        /// </summary>
        [Required(ErrorMessage = "Dealer name is required.")]
        [StringLength(100, ErrorMessage = "Dealer name cannot exceed 100 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// The location of the dealer.
        /// This field is required.
        /// </summary>
        [Required(ErrorMessage = "Dealer location is required.")]
        [StringLength(150, ErrorMessage = "Dealer location cannot exceed 150 characters.")]
        public string Location { get; set; }

        /// <summary>
        /// Converts the DTO to a Dealer model object with the provided dealer ID.
        /// </summary>
        /// <param name="id">The unique ID of the dealer.</param>
        /// <returns>A Dealer model object.</returns>
        public Dealer ToModel(string id)
        {
            return new Dealer
            {
                Id = id,
                Name = Name,
                Location = Location
            };
        }
    }
}
