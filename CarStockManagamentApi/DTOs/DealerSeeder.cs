using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;
using System.Collections.Generic;

namespace CarStockManagamentApi.Data
{
    public class DealerSeeder
    {
        public List<Dealer> SeedDealers(ref int dealerId)
        {
            var dealers = new List<Dealer>
            {
                new Dealer { Id = "1", Name = "Melbourne Toyota", Location = "West Melbourne" },
                new Dealer { Id = "2", Name = "South Morang Mazda", Location = "South Morang" }
            };

            // Update the dealerId based on seeded data
            dealerId = dealers.Count;

            return dealers;
        }
    }
}
