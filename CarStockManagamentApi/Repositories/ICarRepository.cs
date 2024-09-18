using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;
using System.Collections.Generic;

namespace CarStockManagementApi.Repositories
{
    public interface ICarRepository
    {
        /// <summary>
        /// Retrieves all cars associated with a specific dealer.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <returns>A list of cars belonging to the specified dealer.</returns>
        List<Car> GetCarsByDealer(string dealerId);

        /// <summary>
        /// Adds a new car to the specified dealer's inventory.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="carDto">The data transfer object containing car details.</param>
        /// <returns>The added car.</returns>
        Car AddCar(string dealerId, CarDto carDto);

        /// <summary>
        /// Searches for cars based on the specified criteria.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="make">The make of the car (optional).</param>
        /// <param name="model">The model of the car (optional).</param>
        /// <param name="year">The year of the car (optional).</param>
        /// <returns>A list of cars that match the search criteria.</returns>
        List<Car> SearchCars(string dealerId, string? make, string? model, int? year);

        /// <summary>
        /// Removes a car from the specified dealer's inventory.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="car">The car to be removed.</param>
        /// <returns>A list of cars remaining in the dealer's inventory after removal.</returns>
        List<Car> RemoveCar(string dealerId, Car car);

        /// <summary>
        /// Updates the stock quantity of a specific car in the dealer's inventory.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="car">The car to be updated.</param>
        /// <param name="newStock">The new stock quantity.</param>
        /// <returns>The updated car, or null if the car was not found.</returns>
        Car? UpdateCarStock(string dealerId, Car car, int newStock);
    }
}
