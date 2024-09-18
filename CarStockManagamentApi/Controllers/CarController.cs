using CarStockManagamentApi.Data;
using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;

namespace CarStockManagementApi.Controllers
{
    public class CarController
    {
        private readonly DealerRepository _dealerRepository;
        private readonly CarRepository _carRepository;
        private readonly ILogger<CarController> _logger;

        public CarController(DealerRepository dealerRepository, CarRepository carRepository, ILogger<CarController> logger)
        {
            _dealerRepository = dealerRepository;
            _carRepository = carRepository;
            _logger = logger;
        }

        public IResult AddCar(string dealerId, CarDto carDto)
        {
            if (carDto == null)
                return Results.BadRequest(CreateErrorResponse("Car data must be provided."));

            var dealer = GetDealer(dealerId);
            if (dealer == null)
                return Results.NotFound(CreateErrorResponse($"Dealer with ID '{dealerId}' not found."));

            var car = _carRepository.AddCar(dealerId, carDto);
            _logger.LogInformation("Car added for dealer {DealerId}: {CarMake} {CarModel}", dealerId, carDto.Make, carDto.Model);
            return Results.Created($"/dealers/{dealerId}/cars/{car.Id}", car);
        }

        public IResult RemoveCar(string dealerId, string make, string model, int year)
        {
            var dealer = GetDealer(dealerId);
            if (dealer == null)
                return Results.NotFound(CreateErrorResponse("Dealer not found."));

            var result = _carRepository.RemoveCar(dealerId, make, model, year);
            if (result == null)
                return Results.NotFound(CreateErrorResponse("Car not found."));

            _logger.LogInformation("Car removed: {CarMake} {CarModel} for dealer {DealerId}", make, model, dealerId);
            return Results.Ok(result);
        }

        public IResult SearchCars(string dealerId, string? make, string? model, int? year)
        {
            var dealer = GetDealer(dealerId);
            if (dealer == null)
                return Results.NotFound(CreateErrorResponse($"Dealer with ID '{dealerId}' not found."));

            if (string.IsNullOrEmpty(make) && string.IsNullOrEmpty(model) && !year.HasValue)
                return Results.BadRequest(CreateErrorResponse("At least one of the search fields (make, model, year) must be provided."));

            var cars = _carRepository.SearchCars(dealerId, make, model, year);
            if (cars == null || !cars.Any())
                return Results.NotFound(CreateErrorResponse("No cars found matching the search criteria."));

            return Results.Ok(cars);
        }

        public IResult UpdateCarStock(string dealerId, string make, string model, int year, int stockQuantity)
        {
            var dealer = GetDealer(dealerId);
            if (dealer == null)
                return Results.NotFound(CreateErrorResponse($"Dealer with ID '{dealerId}' not found."));

            if (stockQuantity < 0)
                return Results.BadRequest(CreateErrorResponse("Stock quantity must be non-negative."));

            var car = _carRepository.UpdateCarStock(dealerId, make, model, year, stockQuantity);
            if (car == null)
                return Results.NotFound(CreateErrorResponse("Car not found."));

            return Results.Ok(car);
        }

        private Dealer? GetDealer(string dealerId)
        {
            return _dealerRepository.GetDealerByID(dealerId);
        }

        private object CreateErrorResponse(string message)
        {
            return new { Message = message };
        }

    }
}
