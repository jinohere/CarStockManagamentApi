using CarStockManagamentApi.Models;
using System.ComponentModel.DataAnnotations;

namespace CarStockManagamentApi.DTO
{
    public class CarDto
    {
        public string Make { get; set; }

        public string Model { get; set; }

        [Range(1986, 2024)]
        public int Year { get; set; }

        public Car ToModel(string dealerId, int carId, int stockQuantity)
        {
            return new Car
            {
                Id = carId,
                Make = Make,
                Model = Model,
                Year = Year,
                DealerId = dealerId,
                StockQuantity = stockQuantity
            };
        }

    }
}
