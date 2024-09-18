using CarStockManagamentApi.Data;
using CarStockManagamentApi.Models;

namespace CarStockManagementApi.Controllers
{
    public class DealerController
    {
        private readonly DealerRepository _dealerRepository;
        private readonly ILogger<DealerController> _logger;

        public DealerController(DealerRepository dealerRepository)
        {
            _dealerRepository = dealerRepository;
        }

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
