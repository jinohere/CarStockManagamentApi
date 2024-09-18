using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;
using CarStockManagementApi.Repositories;

namespace CarStockManagamentApi.Data
{
    public class DealerRepository : IDealerRepository
    {
        private readonly List<Dealer> _dealers = new();
        private int _dealerId = 0;

        public Dealer AddDealer(DealerDto dealerDto)
        {
            var dealer = dealerDto.ToModel((++_dealerId).ToString());
            _dealers.Add(dealer);
            return dealer;
        }

        public Dealer? GetDealerByID(string dealerId)
        {
            return _dealers.FirstOrDefault(x => x.Id == dealerId);
            
        }

        public Dealer? GetDealerByNameAndLocation(string name, string location)
        {
            return _dealers.FirstOrDefault(x => x.Name == name && x.Location == location);

        }
    }
}
