namespace CarStockManagamentApi.Models
{
    public class Car
    {
            public int Id { get; set; }
            public string DealerId { get; set; }
            public string Make { get; set; } 
            public string Model { get; set; }
            public int Year { get; set; }
            public int StockQuantity { get; set; }

    }
}