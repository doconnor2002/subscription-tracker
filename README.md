# SubTrack

SubTrack is a subscription-tracking REST API built with C# and ASP.NET Core. The project is designed to demonstrate modern .NET backend development, layered architecture, database persistence, RESTful API design, validation, and containerized development.

## Tech Stack

* **C#**
* **.NET 10**
* **ASP.NET Core Web API**
* **Entity Framework Core 10**
* **PostgreSQL**
* **Docker / Docker Compose**
* **Swagger / OpenAPI**
* **Git / GitHub**

## Architecture

SubTrack uses a layered architecture with clear separation of responsibilities:

```text
API → Application → Domain
         ↑
Infrastructure → Application → Domain
```

### Project Structure

```text
SubTrack/
├── SubTrack.Api/
├── SubTrack.Application/
├── SubTrack.Domain/
├── SubTrack.Infrastructure/
└── docker-compose.yml
```

### API

Responsible for:

* HTTP endpoints
* Request/response handling
* HTTP status codes
* Query parameters
* API routing

### Application

Responsible for:

* Business logic
* Service interfaces
* DTOs
* Mapping domain entities to API responses
* Coordinating repository operations

### Domain

Contains the core application models and business concepts without dependencies on infrastructure or API concerns.

### Infrastructure

Responsible for:

* Entity Framework Core
* PostgreSQL persistence
* Database configuration
* Repository implementations
* Data access

## Features

Currently implemented:

* Create subscriptions
* Retrieve a subscription by ID
* Retrieve all subscriptions
* Filter subscriptions by user
* Filter subscriptions by active/inactive status
* Combine subscription filters
* Delete subscriptions
* PostgreSQL persistence
* Docker Compose development environment
* Request validation
* RESTful HTTP status codes
* Read-only database queries using EF Core `AsNoTracking()`

## API Endpoints

### Create Subscription

```http
POST /api/subscriptions
```

Creates a new subscription.

A successful request returns:

```text
201 Created
```

The response includes a `Location` header pointing to the newly created subscription.

### Get Subscription by ID

```http
GET /api/subscriptions/{id}
```

Returns a subscription by its ID.

If the subscription does not exist:

```text
404 Not Found
```

### Get All Subscriptions

```http
GET /api/subscriptions
```

Returns all subscriptions.

### Filter by User

```http
GET /api/subscriptions?userId=1
```

Returns subscriptions belonging to the specified user.

### Filter by Active Status

```http
GET /api/subscriptions?isActive=true
```

Returns only active subscriptions.

To retrieve inactive subscriptions:

```http
GET /api/subscriptions?isActive=false
```

### Combine Filters

The filtering parameters can be used together:

```http
GET /api/subscriptions?userId=1&isActive=true
```

This returns subscriptions that belong to user `1` **and** are active.

Both filters are optional. If neither is provided, all subscriptions are returned.

If no subscriptions match the supplied filters, the API returns an empty collection:

```json
[]
```

### Delete Subscription

```http
DELETE /api/subscriptions/{id}
```

Deletes a subscription by its ID.

A successful deletion returns:

```text
204 No Content
```

If the subscription does not exist:

```text
404 Not Found
```

## Example Subscription Response

```json
{
  "id": 1,
  "userId": 1,
  "name": "Netflix",
  "price": 15.99,
  "nextBillingDate": "2026-11-01",
  "billingFrequency": 1,
  "isActive": true,
  "createdAt": "2026-10-06T20:00:00Z"
}
```

## Running the Project

### Prerequisites

Install:

* .NET 10 SDK
* Docker Desktop
* Git

### Clone the Repository

```bash
git clone https://github.com/doconnor2002/subscription-tracker.git
cd subscription-tracker
```

### Start PostgreSQL

The project uses Docker Compose to run PostgreSQL.

```bash
docker compose up -d
```

Verify that the database container is running:

```bash
docker compose ps
```

### Configure the Application

Create an environment configuration based on the project's environment example.

The application requires a PostgreSQL connection string.

Example:

```text
ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=subtrack;Username=postgres;Password=your_password
```

Do not commit passwords, connection strings containing credentials, or other secrets to source control.

### Run the API

From the solution root:

```bash
dotnet run --project SubTrack.Api
```

The API will start and expose its Swagger/OpenAPI documentation.

## Database

SubTrack uses:

* PostgreSQL for persistence
* Entity Framework Core for data access
* Code-first database development

The Infrastructure layer contains the EF Core `DbContext` and repository implementations.

Read-only collection queries use:

```csharp
.AsNoTracking()
```

to avoid unnecessary change tracking when entities are only being read.

## Repository Pattern

Database operations are abstracted behind repository interfaces in the Application layer.

For example:

```csharp
Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync(
    long? userId,
    bool? isActive);
```

The Infrastructure layer implements the interface and uses EF Core to construct the database query.

Optional filters are applied only when their corresponding query parameter is supplied.

This allows requests such as:

```text
/api/subscriptions
/api/subscriptions?userId=1
/api/subscriptions?isActive=true
/api/subscriptions?userId=1&isActive=true
```

without requiring separate repository methods for every combination.

## Validation and HTTP Responses

The API follows standard REST conventions for common operations.

| Scenario                          | Response          |
| --------------------------------- | ----------------- |
| Subscription created              | `201 Created`     |
| Subscription retrieved            | `200 OK`          |
| Subscription collection retrieved | `200 OK`          |
| Subscription deleted              | `204 No Content`  |
| Subscription not found            | `404 Not Found`   |
| Invalid request                   | `400 Bad Request` |

## Development Workflow

The project is developed using feature branches and pull requests.

Example:

```text
main
  │
  └── feature/subscription-filtering
```

Changes are developed, tested, reviewed, and merged through GitHub pull requests.

GitHub Copilot is also used as an additional code-review tool to identify potential bugs, maintainability concerns, and missing test coverage. Findings are manually evaluated before changes are made.

## Future Improvements

Planned functionality includes:

* Additional subscription management operations
* More comprehensive automated tests
* Improved API validation
* Pagination and sorting
* Authentication and authorization
* User management
* Subscription billing notifications
* Improved API documentation
* CI/CD automation

## License

This project is for personal development and portfolio purposes.
