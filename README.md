# SubTrack

SubTrack is a subscription tracking application built with ASP.NET Core and PostgreSQL. The project is being developed with a layered architecture designed to support future features such as authentication, subscription management, automated renewal processing, and notifications.

## Tech Stack

* C#
* ASP.NET Core Web API
* .NET 10
* Entity Framework Core
* PostgreSQL
* Docker / Docker Compose
* React + TypeScript (planned)
* Azure services (planned)

## Project Structure

```text
SubTrack/
├── SubTrack.Api/              # HTTP API and application entry point
├── SubTrack.Application/      # Application logic and use cases
├── SubTrack.Domain/           # Domain entities and business rules
├── SubTrack.Infrastructure/  # Database and external infrastructure
├── compose.yaml               # Local PostgreSQL configuration
└── SubTrack.sln
```

The intended dependency flow is:

```text
API
 ↓
Application
 ↓
Domain
 ↑
Infrastructure
```

Infrastructure provides the implementations needed by the application, including database access through Entity Framework Core.

## Local Development Setup

### Prerequisites

* .NET 10 SDK
* Docker Desktop
* Git

### 1. Clone the repository

```powershell
git clone <repository-url>
cd subscription-tracker
```

### 2. Create the local environment file

Copy the example environment file:

```powershell
Copy-Item .env.example .env
```

The `.env` file contains the credentials used by the local PostgreSQL container and should not be committed to source control.

If you change the PostgreSQL password in `.env`, make sure the API connection string uses the same password.

### 3. Start PostgreSQL

```powershell
docker compose up -d
```

Verify the container is running:

```powershell
docker compose ps
```

### 4. Configure the API connection string

The API uses .NET User Secrets for the local database connection string.

Initialize User Secrets if they have not already been configured:

```powershell
dotnet user-secrets init --project SubTrack.Api
```

Set the connection string using the same database credentials configured in `.env`:

```powershell
dotnet user-secrets set "ConnectionStrings:SubTrack" "Host=localhost;Port=5432;Database=subtrack;Username=subtrack;Password=change-me" --project SubTrack.Api
```

If you changed the password in `.env`, replace `change-me` with the same password.

User Secrets keeps the API connection string out of source control.

### 5. Apply database migrations

From the repository root:

```powershell
dotnet ef database update --project SubTrack.Infrastructure --startup-project SubTrack.Api
```

### 6. Run the API

```powershell
dotnet run --project SubTrack.Api
```

The API will start on the local development URL shown in the console.

## Database

SubTrack uses PostgreSQL for local development. Entity Framework Core migrations are stored in:

```text
SubTrack.Infrastructure/Migrations/
```

To create a new migration:

```powershell
dotnet ef migrations add <MigrationName> --project SubTrack.Infrastructure --startup-project SubTrack.Api
```

To apply migrations:

```powershell
dotnet ef database update --project SubTrack.Infrastructure --startup-project SubTrack.Api
```

## Development Workflow

Feature work is developed on separate branches and merged into `main` through pull requests.

Before opening a pull request:

```powershell
dotnet build
```

Changes should include appropriate tests as the project grows.
