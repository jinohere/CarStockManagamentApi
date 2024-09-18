using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;

namespace CarStockManagementApi.Repositories
{
    public interface IDealerRepository
    {
        /// <summary>
        /// Adds a new dealer to the repository.
        /// </summary>
        /// <param name="dealerDto">The data transfer object containing dealer information.</param>
        /// <returns>The added dealer.</returns>
        Dealer AddDealer(DealerDto dealerDto);

        /// <summary>
        /// Retrieves a dealer by their unique ID.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <returns>The dealer if found; otherwise, null.</returns>
        Dealer? GetDealerByID(string dealerId);

        /// <summary>
        /// Retrieves a dealer by their name and location.
        /// </summary>
        /// <param name="name">The name of the dealer.</param>
        /// <param name="location">The location of the dealer.</param>
        /// <returns>The dealer if found; otherwise, null.</returns>
        Dealer? GetDealerByNameAndLocation(string name, string location);
    }
}
