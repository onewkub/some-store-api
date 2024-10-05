# Some Store API

This is a .NET Core 8 Web API project using Entity Framework Core (EF Core) for data access. It provides functionality for managing products, customers, orders, and more in a store. This version uses SQLite as the database.

## Table of Contents

- [Some Store API](#some-store-api)
  - [Table of Contents](#table-of-contents)
  - [Requirements](#requirements)
  - [Installation](#installation)
  - [Usage](#usage)
    - [Swagger UI](#swagger-ui)
  - [Database Setup](#database-setup)
  - [API Endpoints](#api-endpoints)

## Requirements

- [.NET Core SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
- SQLite

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/onewkub/some-store-api.git
   cd some-store-api
   ```

2. Restore NuGet packages:

   ```bash
   dotnet restore
   ```

3. Set up the database connection string in `appsettings.json`:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Data Source=SomeStoreDb.sqlite"
   }
   ```

4. Apply database migrations:

   ```bash
   dotnet ef database update
   ```

## Usage

Run the application locally using the following command:

```bash
   dotnet run
```

You can access the API by navigating to `https://localhost:5001` (or the port specified in `launchSettings.json`).

### Swagger UI

The API documentation is available through Swagger. After running the project, navigate to:

```bash
https://localhost:5001/swagger/index.html
```

## Database Setup

This project uses SQLite as the database provider. Entity Framework Core is used for handling migrations. Follow these steps:

1. Add a new migration:

   ```bash
   dotnet ef migrations add InitialCreate
   ```

2. Update the database:

   ```bash
   dotnet ef database update
   ```

## API Endpoints

Here are some of the main API endpoints:

- `GET /api/products` - Retrieves a list of products
- `GET /api/products/sync` - Retrieves products and update

Additional endpoints for customers, orders, and other entities will follow a similar structure.
