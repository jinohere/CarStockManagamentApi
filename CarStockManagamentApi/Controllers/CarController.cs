using CarStockManagamentApi.Data;
using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;

namespace CarStockManagementApi.Controllers
{
    public class CarController
    {
        private readonly DealerRepository _dealerRepository;
        private readonly CarRepository _carRepository;

        public CarController(DealerRepository dealerRepository, CarRepository carRepository)
        {
            _dealerRepository = dealerRepository;
            _carRepository = carRepository;
        }

        public IResult AddCar(string dealerId, CarDto carDto)
        {
            if (carDto == null)
                return Results.BadRequest(CreateErrorResponse("Car data must be provided. Please include all necessary details to add a car."));

            var dealer = GetDealer(dealerId);
            if (dealer == null)
                return Results.NotFound(CreateErrorResponse($"Dealer with ID '{dealerId}' not found. Ensure the dealer ID is correct and try again."));

            var car = _carRepository.AddCar(dealerId, carDto);
            return Results.Created($"/dealers/{dealerId}/cars/{car.Id}", new
            {
                Message = "Car successfully added to dealer's inventory.",
                Car = car
            });
        }

        public IResult RemoveCar(string dealerId, string make, string model, int year)
        {
            var dealer = GetDealer(dealerId);
            if (dealer == null)
                return Results.NotFound(CreateErrorResponse($"Dealer with ID '{dealerId}' not found. Please check the dealer ID and try again."));

            var result = _carRepository.RemoveCar(dealerId, make, model, year);
            if (result == null)
                return Results.NotFound(CreateErrorResponse($"Car {make} {model} ({year}) not found in dealer's inventory."));

            return Results.Ok(new
            {
                Message = "Car successfully removed from dealer's inventory.",
                RemovedCar = result
            });
        }

        public IResult SearchCars(string dealerId, string? make, string? model, int? year)
        {
            var dealer = GetDealer(dealerId);
            if (dealer == null)
                return Results.NotFound(CreateErrorResponse($"Dealer with ID '{dealerId}' not found. Verify the dealer ID and try again."));

            if (string.IsNullOrEmpty(make) && string.IsNullOrEmpty(model) && !year.HasValue)
                return Results.BadRequest(CreateErrorResponse("At least one search parameter (make, model, year) must be provided to search for cars."));

            var cars = _carRepository.SearchCars(dealerId, make, model, year);
            if (cars == null || !cars.Any())
                return Results.NotFound(CreateErrorResponse("No cars found matching the provided search criteria."));

            return Results.Ok(new
            {
                Message = "Search results retrieved successfully.",
                Cars = cars
            });
        }

        public IResult UpdateCarStock(string dealerId, string make, string model, int year, int stockQuantity)
        {
            var dealer = GetDealer(dealerId);
            if (dealer == null)
                return Results.NotFound(CreateErrorResponse($"Dealer with ID '{dealerId}' not found. Please check the dealer ID and try again."));

            if (stockQuantity < 0)
                return Results.BadRequest(CreateErrorResponse("Stock quantity must be a non-negative number. Please provide a valid stock quantity."));

            var car = _carRepository.UpdateCarStock(dealerId, make, model, year, stockQuantity);
            if (car == null)
                return Results.NotFound(CreateErrorResponse($"Car {make} {model} ({year}) not found in dealer's inventory."));

            return Results.Ok(new
            {
                Message = "Car stock updated successfully.",
                Car = car
            });
        }

        private Dealer? GetDealer(string dealerId)
        {
            return _dealerRepository.GetDealerByID(dealerId);
        }

        private static object CreateErrorResponse(string message)
        {
            return new { Error = "Request Failed", Message = message };
        }
    }
}
