namespace CarStockManagamentApi.Models
{
    public class DealerDto
    {
            public string Name { get; set; }
            public string Location { get; set; }

            public Dealer ToModel(string id)
            {
                return new Dealer
                {
                    Id = id,
                    Name = Name,
                    Location = Location,
                };
            }
    }
}