using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;
using CarStockManagementApi.Repositories;

namespace CarStockManagamentApi.Data
{
    public class CarRepository : ICarRepository
    {
        private readonly Dictionary<string, List<Car>> _dealerCars = new();
        private int _carId = 0;

        public List<Car> GetCarsByDealer(string dealerId)
        {
            return _dealerCars.ContainsKey(dealerId) ? _dealerCars[dealerId] : new List<Car>();
        }

        public CarRepository()
        {
            // Initialize with default seed data
            _dealerCars = new Dictionary<string, List<Car>>
        {
            {
                "1", new List<Car> // Dealer with ID "0"
                {
                    new Car { Id = 0, Make = "Audi", Model = "A4", Year = 2018, StockQuantity = 1},
                    new Car { Id = 1, Make = "Audi", Model = "A4", Year = 2017, StockQuantity = 1},
                    new Car { Id = 2, Make = "Toyota", Model = "Rav4", Year = 2020, StockQuantity = 1},
                    new Car { Id = 3, Make = "Toyota", Model = "Corolla", Year = 2020, StockQuantity = 1}
                }
            },
            {
                "2", new List<Car> // Dealer with ID "1"
                {
                    new Car { Id = 0, Make = "Tesla", Model = "model Y", Year = 2021, StockQuantity = 1},
                    new Car { Id = 1, Make = "Honda", Model = "CRV", Year = 2019, StockQuantity = 1},
                    new Car { Id = 2, Make = "Mazda", Model = "CX9", Year = 2019, StockQuantity = 1},
                }
            }
        };

            // Ensure the car ID is in sync with the initial seed data
            _carId = _dealerCars.SelectMany(d => d.Value).Max(c => c.Id);
        }

        public Car AddCar(string dealerId, CarDto carDto)
        {
            // Check if the dealer already has a list of cars
            if (!_dealerCars.ContainsKey(dealerId))
            {
                _dealerCars[dealerId] = new List<Car>();
            }

            // Check for existing car in the dealer's inventory
            var existingCar = _dealerCars[dealerId]
                .FirstOrDefault(c => c.Make == carDto.Make && c.Model == carDto.Model && c.Year == carDto.Year);

            if (existingCar != null)
            {
                // If the car exists, increment the stock quantity
                existingCar.StockQuantity += 1;
                return existingCar; // Return the updated car
            }
            else
            {
                // If the car does not exist, create a new car
                var car = carDto.ToModel(dealerId, ++_carId, 1);

                _dealerCars[dealerId].Add(car); // Add the new car to the dealer's list
                return car; // Return the newly added car
            }
        }


        public List<Car>? RemoveCar(string dealerId, string make, string model, int year)
        {
            if (_dealerCars.ContainsKey(dealerId))
            {
                var carToRemove = _dealerCars[dealerId]
                    .FirstOrDefault(c => c.Make.Equals(make, StringComparison.OrdinalIgnoreCase) &&
                                         c.Model.Equals(model, StringComparison.OrdinalIgnoreCase) &&
                                         c.Year == year);

                if (carToRemove != null)
                {
                    if (carToRemove.StockQuantity > 1)
                    {
                        // Decrement stock by 1
                        carToRemove.StockQuantity -= 1;
                    }
                    else
                    {
                        // Remove the car if stock is 1
                        _dealerCars[dealerId].Remove(carToRemove);
                    }
                    return _dealerCars[dealerId]; // Successfully removed or decremented stock
                }
            }
            return null; // Car not found
        }


        public List<Car> SearchCars(string dealerId, string make, string model, int? year)
        {
            if (!_dealerCars.ContainsKey(dealerId))
            {
                return new List<Car>();
            }

            // Filter cars based on the search criteria
            var filteredCars = _dealerCars[dealerId]
                .Where(c => (make == null || c.Make.Equals(make, StringComparison.OrdinalIgnoreCase)) &&
                             (model == null || c.Model.Equals(model, StringComparison.OrdinalIgnoreCase)) &&
                             (!year.HasValue || c.Year == year.Value))
                .ToList();

            // Group by Make, Model, and Year and sum the StockQuantity
            var uniqueCars = filteredCars
                .GroupBy(c => new { c.Make, c.Model, c.Year })
                .Select(g => new Car
                {
                    Make = g.Key.Make,
                    Model = g.Key.Model,
                    Year = g.Key.Year,
                    DealerId = dealerId,
                    StockQuantity = g.Sum(c => c.StockQuantity)
                })
                .ToList();

            return uniqueCars;
        }


        public Car? UpdateCarStock(string dealerId, string make, string model, int year, int newStock)
        {
            if (_dealerCars.ContainsKey(dealerId))
            {
                var carToUpdate = _dealerCars[dealerId].FirstOrDefault(c => c.Make == make && c.Model == model && c.Year == year);
                if (carToUpdate != null)
                {
                    carToUpdate.StockQuantity = newStock;
                    return carToUpdate;
                }
            }
            return null;
        }

    }
}
