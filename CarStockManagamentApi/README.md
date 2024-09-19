OracleCMS CarStockManagamentApi
Overview
CarStockApi is a .NET Core Web API designed for car dealers to efficiently manage their car inventory. It provides functionality for adding, removing, updating, and retrieving car information, as well as searching for cars by make, model, and stock.

Features
Display All Dealers: View a list of all registered dealers.
Add Dealers: Add new dealers to the system.
Add Cars: Add cars to a dealer's inventory.
Search Cars: Search for cars by make, model, and year, and retrieve stock information.
Remove Cars: Remove cars from a dealer's inventory.
Update Stock: Update the stock quantity of existing cars.
In-Memory Database: Utilize an in-memory database for rapid testing and development.
Swagger UI: Explore and test the API using Swagger.

Dependencies
.NET SDK 8.0
Docker (optional, for containerization)
Cloning the Repository
To clone the repository, use the following command:

Getting Started
Clone the Repository
To get started, clone the repository to your local machine:

bash
Copy code
git clone https://github.com/jinohere/CarStockManagamentApi.git
cd CarStockManagamentApi
Building the Project
Before running the application, you need to build the project. Open your terminal and execute:

bash
Copy code
dotnet build
Running the Application
Instead of using the command line to run the application, you can directly open the project in Visual Studio. Follow these steps:

Open Visual Studio.
Click on "Open a project or solution" and navigate to the cloned CarStockManagamentApi directory.
Select the .csproj file and click "Open".
Once the project is loaded, build the solution by clicking on Build > Build Solution.
Run the project by clicking the Start button (or press F5).
Accessing the API
Once the application is running, you can access the API documentation and testing interface via Swagger at:

bash
Copy code
http://localhost:5077/swagger/index.html

Author
Jino Mathew
Email: jinohere@gmail.com