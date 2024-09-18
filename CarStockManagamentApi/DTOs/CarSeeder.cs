using CarStockManagamentApi.Models;
using System.Collections.Generic;

namespace CarStockManagamentApi.Data
{
    public class CarSeeder
    {
        public Dictionary<string, List<Car>> SeedCars(ref int carId)
        {
            var dealerCars = new Dictionary<string, List<Car>>
            {
                {
                    "1", new List<Car>
                    {
                        new Car { Id = carId++, DealerId = "1", Make = "Audi", Model = "A4", Year = 2018, StockQuantity = 1 },
                        new Car { Id = carId++, DealerId = "1", Make = "Audi", Model = "A4", Year = 2017, StockQuantity = 3 },
                        new Car { Id = carId++, DealerId = "1", Make = "Toyota", Model = "Rav4", Year = 2020, StockQuantity = 1 },
                        new Car { Id = carId++, DealerId = "1", Make = "Toyota", Model = "Corolla", Year = 2020, StockQuantity = 4 }
                    }
                },
                {
                    "2", new List<Car>
                    {
                        new Car { Id = carId++, DealerId = "2", Make = "Tesla", Model = "Model Y", Year = 2021, StockQuantity = 1 },
                        new Car { Id = carId++, DealerId = "2", Make = "Honda", Model = "CRV", Year = 2019, StockQuantity = 8 },
                        new Car { Id = carId++, DealerId = "2", Make = "Mazda", Model = "CX-9", Year = 2019, StockQuantity = 1 }
                    }
                }
            };

            return dealerCars;
        }
    }
}
