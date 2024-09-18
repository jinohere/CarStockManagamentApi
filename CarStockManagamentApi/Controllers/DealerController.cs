using CarStockManagamentApi.Data;
using CarStockManagamentApi.Models;

namespace CarStockManagementApi.Controllers
{
    public class DealerController
    {
        private readonly DealerRepository _dealerRepository;

        public DealerController(DealerRepository dealerRepository)
        {
            _dealerRepository = dealerRepository;
        }

        /// <summary>
        /// Retrieves all dealers from the repository.
        /// </summary>
        /// <returns>
        /// A list of dealers if found, or a 404 Not Found response if no dealers exist in the system.
        /// </returns>
        public IResult GetAllDealers()
        {
            var dealers = _dealerRepository.GetAllDealers();
            if (dealers == null || !dealers.Any())
            {
                return Results.NotFound(CreateErrorResponse("No dealers were found in the system. Please add dealers to proceed."));
            }
            return Results.Ok(new
            {
                Message = "Dealers retrieved successfully.",
                Dealers = dealers
            });
        }

        /// <summary>
        /// Adds a new dealer to the repository.
        /// </summary>
        /// <param name="dealerDto">The DTO object containing the details of the dealer to be added. This must not be null.</param>
        /// <returns>
        /// A 201 Created response containing the details of the newly added dealer if successful,
        /// or an error response if validation fails.
        /// 
        /// - Returns 400 Bad Request if the dealer data is null.
        /// - Returns 409 Conflict if a dealer with the same name and location already exists.
        /// </returns>
        public IResult AddDealer(DealerDto dealerDto)
        {
            if (dealerDto == null)
                return Results.BadRequest(CreateErrorResponse("Dealer data must be provided and cannot be null. Please check your input."));

            var existingDealer = _dealerRepository.GetDealerByNameAndLocation(dealerDto.Name, dealerDto.Location);
            if (existingDealer != null)
                return Results.Conflict(CreateErrorResponse("A dealer with the same name and location already exists. Please use a different name or location."));

            var dealer = _dealerRepository.AddDealer(dealerDto);
            return Results.Created($"/dealers/{dealer.Id}", new
            {
                Message = "Dealer added successfully.",
                Dealer = dealer
            });
        }

        private static object CreateErrorResponse(string message)
        {
            return new
            {
                Error = "Request Error",
                Message = message
            };
        }
    }
}
