using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;
using CarStockManagementApi.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CarStockManagamentApi.Data
{
    public class DealerRepository : IDealerRepository
    {
        private readonly List<Dealer> _dealers;
        private int _dealerId;

        public DealerRepository()
        {
            // Initialize with default seed data for dealers
            _dealers = new List<Dealer>
            {
                new Dealer { Id = "1", Name = "Melbourne Toyota", Location = "West Melbourne" },
                new Dealer { Id = "2", Name = "South Morang Mazda", Location = "South Morang" }
            };

            // Ensure the dealer ID is in sync with the initial seed data
            _dealerId = _dealers.Max(d => int.Parse(d.Id));
        }

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
            return _dealers.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                                                 x.Location.Equals(location, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Dealer> GetAllDealers()
        {
            return _dealers.AsReadOnly(); // Return a read-only collection
        }
    }
}
