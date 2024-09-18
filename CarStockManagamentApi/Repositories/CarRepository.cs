using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;
using CarStockManagementApi.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CarStockManagamentApi.Data
{
    public class CarRepository : ICarRepository
    {
        private readonly Dictionary<string, List<Car>> _dealerCars;
        private int _carId;

        public CarRepository()
        {
            _dealerCars = new Dictionary<string, List<Car>>
            {
                {
                    "1", new List<Car>
                    {
                        new Car { Id = 0, DealerId = "1", Make = "Audi", Model = "A4", Year = 2018, StockQuantity = 1 },
                        new Car { Id = 1, DealerId = "1", Make = "Audi", Model = "A4", Year = 2017, StockQuantity = 3 },
                        new Car { Id = 2, DealerId = "1", Make = "Toyota", Model = "Rav4", Year = 2020, StockQuantity = 1 },
                        new Car { Id = 3, DealerId = "1", Make = "Toyota", Model = "Corolla", Year = 2020, StockQuantity = 4 }
                    }
                },
                {
                    "2", new List<Car>
                    {
                        new Car { Id = 0, DealerId = "2", Make = "Tesla", Model = "Model Y", Year = 2021, StockQuantity = 1 },
                        new Car { Id = 1, DealerId = "2", Make = "Honda", Model = "CRV", Year = 2019, StockQuantity = 8 },
                        new Car { Id = 2, DealerId = "2", Make = "Mazda", Model = "CX-9", Year = 2019, StockQuantity = 1 }
                    }
                }
            };

            // Ensure the car ID is in sync with the initial seed data
            _carId = _dealerCars.SelectMany(d => d.Value).Max(c => c.Id);
        }

        public List<Car> GetCarsByDealer(string dealerId)
        {
            return _dealerCars.TryGetValue(dealerId, out var cars) ? cars : new List<Car>();
        }

        public Car AddCar(string dealerId, CarDto carDto)
        {
            if (carDto == null)
                throw new ArgumentNullException(nameof(carDto), "Car data cannot be null.");

            if (!_dealerCars.ContainsKey(dealerId))
            {
                _dealerCars[dealerId] = new List<Car>();
            }

            var existingCar = _dealerCars[dealerId]
                .FirstOrDefault(c => c.Make.Equals(carDto.Make, StringComparison.OrdinalIgnoreCase) &&
                                     c.Model.Equals(carDto.Model, StringComparison.OrdinalIgnoreCase) &&
                                     c.Year == carDto.Year);

            if (existingCar != null)
            {
                existingCar.StockQuantity += 1; // Increment stock quantity
                return existingCar;
            }
            else
            {
                var car = carDto.ToModel(dealerId, ++_carId, 1);
                _dealerCars[dealerId].Add(car);
                return car;
            }
        }

        public List<Car>? RemoveCar(string dealerId, Car carToRemove)
        {
            if (carToRemove == null) return null;

            if (_dealerCars.TryGetValue(dealerId, out var cars))
            {
                if (carToRemove.StockQuantity > 1)
                {
                    carToRemove.StockQuantity -= 1; // Decrement stock by 1
                }
                else
                {
                    cars.Remove(carToRemove); // Remove the car if stock is 1
                }
                return cars; // Return the updated list
            }

            return null; // Dealer ID not found
        }

        public List<Car> SearchCars(string dealerId, string make, string model, int? year)
        {
            if (!_dealerCars.TryGetValue(dealerId, out var cars))
                return new List<Car>();

            // Perform case-insensitive filtering
            var filteredCars = cars
                .Where(c => (string.IsNullOrEmpty(make) || c.Make.Equals(make, StringComparison.OrdinalIgnoreCase)) &&
                             (string.IsNullOrEmpty(model) || c.Model.Equals(model, StringComparison.OrdinalIgnoreCase)) &&
                             (!year.HasValue || c.Year == year.Value))
                .ToList();

            // Group by Make, Model, and Year, and sum the StockQuantity
            return filteredCars
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
        }

        public Car? UpdateCarStock(string dealerId, Car carToUpdate, int newStock)
        {
            if (carToUpdate != null)
            {
                carToUpdate.StockQuantity = newStock;
                return carToUpdate;
            }
            return null; // Car not found
        }

        public Car? GetCarByMakeModelYear(string dealerId, string make, string model, int year)
        {
            if (_dealerCars.TryGetValue(dealerId, out var cars))
            {
                return cars.FirstOrDefault(x =>
                    x.Make.Equals(make, StringComparison.OrdinalIgnoreCase) &&
                    x.Model.Equals(model, StringComparison.OrdinalIgnoreCase) &&
                    x.Year == year);
            }

            return null; // Dealer ID not found
        }
    }
}
