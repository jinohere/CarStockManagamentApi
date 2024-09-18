using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;

namespace CarStockManagementApi.Validator
{
    public class DealerValidator
    {
        public IResult ValidateAddCar(string dealerId, CarDto carDto, Dealer? dealer)
        {
            var dealerValidationResult = ValidateDealer(dealerId, dealer);
            if (dealerValidationResult != null)
            {
                return dealerValidationResult;
            }

            if (carDto == null)
            {
                return Results.BadRequest(CreateErrorResponse("Car data must be provided. Please include all necessary details to add a car."));
            }

            return null; // No validation errors
        }

        public IResult ValidateSearchCars(string dealerId, string? make, string? model, int? year, Dealer? dealer)
        {
            var dealerValidationResult = ValidateDealer(dealerId, dealer);
            if (dealerValidationResult != null)
            {
                return dealerValidationResult;
            }

            if (string.IsNullOrEmpty(make) && string.IsNullOrEmpty(model) && !year.HasValue)
            {
                return Results.BadRequest(CreateErrorResponse("At least one search parameter (make, model, year) must be provided to search for cars."));
            }

            return null; // No validation errors
        }

        public IResult ValidateRemoveCar(string dealerId, Car? car, Dealer? dealer)
        {
            var dealerValidationResult = ValidateDealer(dealerId, dealer);
            if (dealerValidationResult != null)
            {
                return dealerValidationResult;
            }

            if (car == null)
            {
                return Results.NotFound(CreateErrorResponse("Car not found in dealer's inventory."));
            }

            return null; // No validation errors
        }

        public IResult ValidateUpdateCarStock(string dealerId, Car? car, Dealer? dealer, int stockQuantity)
        {
            var dealerValidationResult = ValidateDealer(dealerId, dealer);
            if (dealerValidationResult != null)
            {
                return dealerValidationResult;
            }

            if (stockQuantity < 0)
            {
                return Results.BadRequest(CreateErrorResponse("Stock quantity must be a non-negative number. Please provide a valid stock quantity."));
            }

            if (car == null)
            {
                return Results.NotFound(CreateErrorResponse("Car not found in dealer's inventory."));
            }

            return null; // No validation errors
        }

        private IResult? ValidateDealer(string dealerId, Dealer? dealer)
        {
            if (dealer == null)
            {
                return Results.NotFound(CreateErrorResponse($"Dealer with ID '{dealerId}' not found. Please check the dealer ID and try again."));
            }
            return null; // No dealer validation errors
        }

        private static object CreateErrorResponse(string message)
        {
            return new { Error = "Request Error", Message = message };
        }
    }
}
