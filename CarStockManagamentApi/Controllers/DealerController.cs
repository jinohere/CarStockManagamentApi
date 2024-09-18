using CarStockManagamentApi.Data;
using CarStockManagamentApi.Models;

namespace CarStockManagementApi.Controllers
{
    public class DealerController
    {
        private readonly DealerRepository _dealerRepository;
        private readonly ILogger<DealerController> _logger;

        public DealerController(DealerRepository dealerRepository, ILogger<DealerController> logger)
        {
            _dealerRepository = dealerRepository;
            _logger = logger;
        }

        public IResult AddDealer(DealerDto dealerDto)
        {
            if (dealerDto == null)
                return Results.BadRequest("Dealer data must be provided.");

            var existingDealer = _dealerRepository.GetDealerByNameAndLocation(dealerDto.Name, dealerDto.Location);
            if (existingDealer != null)
                return Results.Conflict(new { Message = "Dealer already exists." });

            var dealer = _dealerRepository.AddDealer(dealerDto);
            _logger.LogInformation("Dealer added: {DealerName}", dealerDto.Name);
            return Results.Created($"/dealers/{dealer.Id}", dealer);
        }
    }
}
