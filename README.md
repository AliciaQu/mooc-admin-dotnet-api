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
