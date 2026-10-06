# SubTrack

SubTrack is a subscription-tracking Web API built with ASP.NET Core, .NET 10, and PostgreSQL. It uses a layered architecture to separate the API, application logic, domain model, and infrastructure.

The current API supports creating subscriptions, retrieving a subscription by ID, and retrieving all subscriptions. Authentication, user management, subscription updates, renewal processing, and notifications are not implemented yet.

## Tech stack

- C#
- ASP.NET Core Web API on .NET 10
- Entity Framework Core 10
- PostgreSQL
- Docker Compose

## Repository layout

```text
subscription-tracker/

├── SubTrack.Api/             # HTTP API and application entry point
├── SubTrack.Application/    # Application services, interfaces, and DTOs
├── SubTrack.Domain/         # Domain entities and enums
├── SubTrack.Infrastructure/ # PostgreSQL, EF Core, and repository implementations
├── .env.example              # Local PostgreSQL environment template
├── compose.yaml              # Local PostgreSQL service
└── SubTrack.slnx             # .NET solution