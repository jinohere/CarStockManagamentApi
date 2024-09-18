using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;

namespace CarStockManagementApi.Repositories
{
    public interface ICarRepository
    {
        List<Car> GetCarsByDealer(string dealerId);
        Car AddCar(string dealerId, CarDto carDto);
        List<Car> RemoveCar(string dealerId, string make, string model, int year);
        List<Car> SearchCars(string dealerId, string make, string model, int? year);
        Car? UpdateCarStock(string dealerId, string make, string model, int year, int newStock);
    }
}