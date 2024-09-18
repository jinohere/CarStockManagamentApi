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

        public DealerRepository()
        {
            // Initialize with default seed data for dealers
            _dealers = new List<Dealer>
        {
            new Dealer { Id = "1", Name = "Melbourne Toyota", Location = "West Melbourne"},
            new Dealer { Id = "2", Name = "South Morang Mazda", Location = "South Morang"}
        };

            // Ensure the dealer ID is in sync with the initial seed data
            _dealerId = _dealers.Max(d => int.Parse(d.Id));
        }

        public Dealer? GetDealerByID(string dealerId)
        {
            return _dealers.FirstOrDefault(x => x.Id == dealerId);
            
        }

        public Dealer? GetDealerByNameAndLocation(string name, string location)
        {
            return _dealers.FirstOrDefault(x => x.Name == name && x.Location == location);

        }

        public IEnumerable<Dealer> GetAllDealers()
        {
            return _dealers;
        }
    }
}
