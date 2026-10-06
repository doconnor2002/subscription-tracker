# SubTrack

SubTrack is a subscription-tracking Web API built with ASP.NET Core, .NET 10, and PostgreSQL. It uses a layered architecture to separate the API, application logic, domain model, and infrastructure.

The current API supports creating subscriptions and retrieving a subscription by ID. Authentication, user management, subscription collection listing and updates, renewal processing, and notifications are not implemented yet.

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
├── SubTrack.Application/     # Application services, interfaces, and DTOs
├── SubTrack.Domain/          # Domain entities and enums
├── SubTrack.Infrastructure/  # PostgreSQL, EF Core, and repository implementations
├── .env.example              # Local PostgreSQL environment template
├── compose.yaml              # Local PostgreSQL service
└── SubTrack.slnx             # .NET solution
```

The API depends on the Application layer, and Infrastructure provides the repository and database implementations used by Application.

## Prerequisites

- .NET 10 SDK
- Docker Desktop with Docker Compose
- EF Core command-line tool (`dotnet-ef` 10.x) for database migrations

Install the EF Core command-line tool if needed:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.12
```

## Local development

Run the following commands from the repository root.

### 1. Configure PostgreSQL

Create a local environment file from the template:

```powershell
Copy-Item .env.example .env
```

The template uses `change-me` as a development-only password. Change it if desired; `.env` is ignored by Git and should not be committed.

Start the database:

```powershell
docker compose up -d
docker compose ps
```

Compose exposes PostgreSQL on `localhost:5432` and stores its data in a named volume.

### 2. Configure the API connection string

The API reads the `ConnectionStrings:SubTrack` connection string from .NET User Secrets in Development. Set its database, username, and password to match `.env`. Replace `REPLACE_WITH_POSTGRES_PASSWORD` in the command below with the value of `POSTGRES_PASSWORD` in your `.env` file:

```powershell
dotnet user-secrets set "ConnectionStrings:SubTrack" "Host=localhost;Port=5432;Database=subtrack;Username=subtrack;Password=REPLACE_WITH_POSTGRES_PASSWORD" --project SubTrack.Api
```

If you changed any PostgreSQL settings in `.env`, update the connection string to match. User Secrets keeps this connection string out of source control.

### 3. Apply database migrations

```powershell
dotnet ef database update --project SubTrack.Infrastructure --startup-project SubTrack.Api
```

### 4. Run the API

```powershell
dotnet run --project SubTrack.Api
```

The HTTP development profile listens at `http://localhost:5234`. The HTTPS profile also listens at `https://localhost:7057`.

## API

### Create a subscription

`POST /api/subscriptions`

Example request:

```json
{
  "userId": 1,
  "name": "Music service",
  "price": 10.99,
  "nextBillingDate": "2026-11-05",
  "billingFrequency": 1
}
```

`billingFrequency` is a numeric enum: `1` for monthly, `2` for quarterly, and `3` for yearly. A successful request returns `201 Created` with the created subscription, including its generated `id`, active status, and creation timestamp.

The response includes a `Location` header pointing to the created subscription's `GET /api/subscriptions/{id}` endpoint.

### Get a subscription by ID

`GET /api/subscriptions/{id}`

For example:

```http
GET /api/subscriptions/1
```

Returns `200 OK` with the subscription when it exists, or `404 Not Found` when no subscription has that ID.

Authentication and user management are not implemented. The request currently supplies `userId` directly; the API does not authenticate or verify that user.

## Database migrations

Migrations are in `SubTrack.Infrastructure/Migrations/`. Create a migration with:

```powershell
dotnet ef migrations add <MigrationName> --project SubTrack.Infrastructure --startup-project SubTrack.Api
```

Apply migrations to the configured database with:

```powershell
dotnet ef database update --project SubTrack.Infrastructure --startup-project SubTrack.Api
```

## Build

Build the solution from the repository root:

```powershell
dotnet build
```
