# RisedorApi - Vendor-Supermarket Order API

A .NET Core Minimal API project following Domain-Driven Design principles.

## Project Structure

```
RisedorApi/
├── RisedorApi.Api/              # Presentation Layer
│   └── Endpoints/               # API Endpoints organization
│
├── RisedorApi.Domain/           # Domain Layer
│   ├── Entities/                # Domain Entities
│   ├── ValueObjects/            # Value Objects
│   └── Enums/                   # Enumerations
│
├── RisedorApi.Application/      # Application Layer
│   ├── Commands/                # CQRS Commands
│   ├── Queries/                 # CQRS Queries
│   ├── Handlers/                # Command and Query Handlers
│   └── Interfaces/              # Interfaces/Contracts
│
├── RisedorApi.Infrastructure/   # Infrastructure Layer
│   ├── Persistence/            # Database Context and Configurations
│   ├── Repositories/           # Repository Implementations
│   └── Services/               # External Service Integrations
│
└── RisedorApi.Shared/          # Shared Kernel
    ├── Common/                 # Shared Utilities and Helpers
    ├── Exceptions/            # Custom Exception Definitions
    └── Middleware/            # Custom Middleware Components
```

## Getting Started

1. Clone the repository
2. Ensure you have .NET 8.0 SDK installed
3. Run `dotnet restore` to restore dependencies
4. Run `dotnet build` to build the solution
5. Run `dotnet run --project RisedorApi.Api` to start the API

## Architecture

This project follows Clean Architecture and Domain-Driven Design principles:

- **Domain Layer**: Contains business entities, value objects, and domain logic
- **Application Layer**: Contains application logic, CQRS implementation
- **Infrastructure Layer**: Contains external concerns and implementations
- **API Layer**: Contains minimal API endpoints and configurations
- **Shared Layer**: Contains cross-cutting concerns
