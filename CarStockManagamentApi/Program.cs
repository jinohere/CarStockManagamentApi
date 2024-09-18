using CarStockManagamentApi.Data;
using CarStockManagamentApi.DTO;
using CarStockManagamentApi.Models;
using CarStockManagementApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DealerRepository>();
builder.Services.AddSingleton<CarRepository>();
builder.Services.AddLogging();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// Dealer Endpoints
var dealerController = new DealerController(app.Services.GetRequiredService<DealerRepository>());
var carController = new CarController(app.Services.GetRequiredService<DealerRepository>(), app.Services.GetRequiredService<CarRepository>());

// Map the endpoints
app.MapGet("/dealers", () => dealerController.GetAllDealers());
app.MapPost("/dealers", (DealerDto dealerDto) => dealerController.AddDealer(dealerDto));
app.MapPost("{dealerId}/cars", (string dealerId, CarDto carDto) => carController.AddCar(dealerId, carDto));
app.MapDelete("{dealerId}/cars", (string dealerId, string make, string model, int year) => carController.RemoveCar(dealerId, make, model, year));
app.MapGet("{dealerId}/cars", (string dealerId, string? make, string? model, int? year) => carController.SearchCars(dealerId, make, model, year));
app.MapPut("{dealerId}/stocks", (string dealerId, string make, string model, int year, int stockQuantity) => carController.UpdateCarStock(dealerId, make, model, year, stockQuantity));


// Run the application
app.Run();