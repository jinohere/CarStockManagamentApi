using CarStockManagamentApi.Data;
using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;
using CarStockManagementApi.Validator;

namespace CarStockManagementApi.Controllers
{
    public class CarController
    {
        private readonly DealerRepository _dealerRepository;
        private readonly CarRepository _carRepository;
        private readonly CarValidator _carValidator;

        public CarController(DealerRepository dealerRepository, CarRepository carRepository)
        {
            _dealerRepository = dealerRepository;
            _carRepository = carRepository;
            _carValidator = new CarValidator();
        }

        /// <summary>
        /// Adds a new car to the specified dealer's inventory.
        /// </summary>
        /// <param name="dealerId">
        /// The ID of the dealer to which the car will be added. This must be a valid, existing dealer ID.
        /// </param>
        /// <param name="carDto">
        /// A DTO object containing the details of the car (e.g., Make, Model, Year). This must not be null.
        /// </param>
        /// <returns>
        /// A 201 Created response containing the newly added car's details if successful, or an error response if validation fails.
        /// 
        /// - Returns 400 Bad Request if the car data is null.
        /// - Returns 404 Not Found if the dealer is not found.
        /// </returns>
        public IResult AddCar(string dealerId, CarDto carDto)
        {
            var dealer = _dealerRepository.GetDealerByID(dealerId);
            var validationResult = _carValidator.ValidateAddCar(dealerId, carDto, dealer);
            if (validationResult != null)
            {
                return validationResult; // Return validation error
            }

            var car = _carRepository.AddCar(dealerId, carDto);
            return Results.Created($"/dealers/{dealerId}/cars/{car.Id}", new
            {
                Message = "Car successfully added to dealer's inventory.",
                Car = car
            });
        }


        /// <summary>
        /// Searches for cars in a dealer's inventory based on optional criteria: make, model, and year.
        /// At least one search parameter must be provided.
        /// </summary>
        /// <param name="dealerId">
        /// The ID of the dealer to search within. Must be a valid, existing dealer ID.
        /// </param>
        /// <param name="make">
        /// Optional. The make (brand) of the car (e.g., "Toyota"). If null or empty, this criterion will be ignored.
        /// </param>
        /// <param name="model">
        /// Optional. The model of the car (e.g., "Corolla"). If null or empty, this criterion will be ignored.
        /// </param>
        /// <param name="year">
        /// Optional. The year of manufacture of the car. If null, this criterion will be ignored.
        /// </param>
        /// <returns>
        /// A 200 OK response with a list of cars that match the search criteria, or an error response if validation fails.
        /// 
        /// - Returns 400 Bad Request if no search criteria are provided.
        /// - Returns 404 Not Found if the dealer or no cars match the search criteria.
        /// </returns>
        public IResult SearchCars(string dealerId, string? make, string? model, int? year)
        {
            var dealer = _dealerRepository.GetDealerByID(dealerId);
            var validationResult = _carValidator.ValidateSearchCars(dealerId, make, model, year, dealer);
            if (validationResult != null)
            {
                return validationResult; // Return validation error
            }

            var cars = _carRepository.SearchCars(dealerId, make, model, year);
            if (cars == null || !cars.Any())
                return Results.NotFound(CreateErrorResponse("No cars found matching the provided search criteria."));

            return Results.Ok(new
            {
                Message = "Search results retrieved successfully.",
                Cars = cars
            });
        }

        /// <summary>
        /// Removes a car from the specified dealer's inventory based on the make, model, and year of the car.
        /// </summary>
        /// <param name="dealerId">
        /// The ID of the dealer from which the car will be removed. Must be a valid, existing dealer ID.
        /// </param>
        /// <param name="make">
        /// The make (brand) of the car to be removed (e.g., "Toyota").
        /// </param>
        /// <param name="model">
        /// The model of the car to be removed (e.g., "Corolla").
        /// </param>
        /// <param name="year">
        /// The year of manufacture of the car to be removed.
        /// </param>
        /// <returns>
        /// A 200 OK response with the details of the removed car, or an error response if validation fails.
        /// 
        /// - Returns 404 Not Found if the dealer or car is not found.
        /// </returns>
        public IResult RemoveCar(string dealerId, string make, string model, int year)
        {
            var dealer = _dealerRepository.GetDealerByID(dealerId);
            var car = _carRepository.GetCarByMakeModelYear(dealerId, make, model, year);
            // Validate the dealer and car
            var validationResult = _carValidator.ValidateRemoveCar(dealerId, car, dealer);
            if (validationResult != null)
            {
                return validationResult; // Return validation error
            }

            var result = _carRepository.RemoveCar(dealerId, car);
            

            return Results.Ok(new
            {
                Message = "Car successfully removed from dealer's inventory.",
                StockAfterRemoveCar = result
            });
        }

        /// <summary>
        /// Updates the stock quantity of a specified car in a dealer's inventory.
        /// Ensures the stock quantity is non-negative before updating.
        /// </summary>
        /// <param name="dealerId">
        /// The unique identifier of the dealer. Must be a valid dealer ID.
        /// </param>
        /// <param name="make">
        /// The make (brand) of the car (e.g., "Toyota"). This is a required field.
        /// </param>
        /// <param name="model">
        /// The model of the car (e.g., "Corolla"). This is a required field.
        /// </param>
        /// <param name="year">
        /// The year of manufacture of the car. This is a required field and should be valid for the car model.
        /// </param>
        /// <param name="stockQuantity">
        /// The updated stock quantity for the car. It must be a non-negative integer.
        /// </param>
        /// <returns>
        /// - Returns 200 OK with the updated car information if successful.
        /// - Returns 400 Bad Request if the stock quantity is invalid (negative).
        /// - Returns 404 Not Found if the dealer or car is not found.
        /// </returns>
        public IResult UpdateCarStock(string dealerId, string make, string model, int year, int stockQuantity)
        {
            var dealer = _dealerRepository.GetDealerByID(dealerId);
 
            var car = _carRepository.GetCarByMakeModelYear(dealerId, make, model, year);
            // Validate the dealer, car, and stock quantity
            var validationResult = _carValidator.ValidateUpdateCarStock(dealerId, car, dealer, stockQuantity);
            if (validationResult != null)
            {
                return validationResult; // Return validation error
            }
            if (car == null)
                return Results.NotFound(CreateErrorResponse($"Car {make} {model} ({year}) not found in dealer's inventory."));
            var updatedStock = _carRepository.UpdateCarStock(dealerId, car, stockQuantity);

            return Results.Ok(new
            {
                Message = "Car stock updated successfully.",
                Car = updatedStock
            });
        }

        private static object CreateErrorResponse(string message)
        {
            return new { Error = "Request Failed", Message = message };
        }
    }
}
