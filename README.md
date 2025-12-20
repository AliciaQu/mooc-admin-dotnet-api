# MOOC Admin – .NET Backend API

This repository contains the **backend API** for a MOOC (Massive Open Online Course) **admin management system**,  
built with **ASP.NET Core Web API (.NET 8)**.  
It is designed for **team collaboration**, **learning purposes**, and **course projects**.

---

## Tech Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- MySQL 8.x
- Autofac (Dependency Injection)
- Swagger / OpenAPI
- JWT Authentication
- NLog (Logging)

---

## Project Structure

```text
.
├── MoocWebApi.sln
├── MoocWebApi/                 # Web API startup project
│   ├── Controllers/
│   ├── Filters/
│   ├── Middlewares/
│   ├── Init/
│   ├── appsettings.json
│   ├── appsettings.Development.example.json
│   └── Program.cs
│
├── Mooc.Application/            # Application layer (business logic)
├── Mooc.Application.Contracts/ # DTOs & service interfaces
├── Mooc.Core/                  # Core infrastructure
├── Mooc.Model/                 # EF Core entities & migrations
├── Mooc.Shared/                # Shared enums & constants
└── Mooc.UnitTest/              # Unit tests


Prerequisites

Make sure you have the following installed:

.NET SDK 8.0+

MySQL 8.x

Git

Optional:

MySQL Workbench

Visual Studio / Rider / VS Code

Environment Setup
1. Clone the repository
git clone https://github.com/AliciaQu/mooc-admin-dotnet-api.git
cd mooc-admin-dotnet-api

2. Configure local environment variables

⚠️ Local secrets are not committed to Git

Each developer must create their own local config file.

cd MoocWebApi
cp appsettings.Development.example.json appsettings.Development.json


Update your database connection string:

{
  "DataBaseOption": {
    "Type": "MySql",
    "ConnectionString": "server=localhost;Database=mooc27;User=root;Password=YOUR_PASSWORD;"
  }
}


appsettings.Development.json is ignored by .gitignore

Database Setup
1. Create database
CREATE DATABASE mooc27
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;

2. Apply migrations
dotnet ef database update \
  --startup-project MoocWebApi/MoocWebApi.csproj \
  --project Mooc.Model/Mooc.Model.csproj

Run the Application
dotnet run --project MoocWebApi/MoocWebApi.csproj


API will be available at:

http://localhost:9000

Swagger UI: http://localhost:9000/swagger

Development Notes

Do NOT commit appsettings.Development.json

Use appsettings.Development.example.json as a template

Each developer uses their own local database

Migrations are shared via Git

Seed data runs automatically on startup

Common Commands
# Run API
dotnet run --project MoocWebApi/MoocWebApi.csproj

# Apply migrations
dotnet ef database update \
  --startup-project MoocWebApi/MoocWebApi.csproj \
  --project Mooc.Model/Mooc.Model.csproj

# Add new migration
dotnet ef migrations add MigrationName \
  --startup-project MoocWebApi/MoocWebApi.csproj \
  --project Mooc.Model/Mooc.Model.csproj
