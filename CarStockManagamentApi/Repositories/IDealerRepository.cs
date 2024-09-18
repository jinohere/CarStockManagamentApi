using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;

namespace CarStockManagementApi.Repositories
{
    public interface IDealerRepository
    {
        Dealer AddDealer(DealerDto dealerDto);
        Dealer? GetDealerByID(string dealerId);
        Dealer?  GetDealerByNameAndLocation(string name, string location);
    }
}