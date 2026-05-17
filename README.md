# CubicSaas — Mini SaaS Tenant Management API

A multi-tenant RESTful API built with ASP.NET Core, Entity Framework Core, and SQL Server.

---

## Tech Stack

- ASP.NET Core (.NET 8)
- Entity Framework Core + SQL Server
- JWT Bearer Authentication
- Hangfire (background jobs)
- FluentValidation
- AutoMapper

---

## Architecture

The solution follows Onion Architecture with 4 layers:

- **Cubic.Core** — Entities and Repositories Interfaces only
- **Cubic.Application** — Services, DTOs, Validators, and Buisness logic
- **Cubic.Infrastructure** — EF Core, Repositories implmentations , Unit of Work and Generic Repository
- **Cubic.Api** — Controllers, Middleware (GlobalException and TenantResolution)

Dependencies always point inward toward Core.

---

## Setup Instructions

1. Clone the repository
2. Apply migrations
3. Run the project
4. Open Swagger at `https://localhost:{port}/swagger`
5. For Hangfire open `https://localhost:{port}/hangfire`
---

## API Endpoints

### Auth (public)
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/auth/login | Login with user Id and receive JWT token |

*** Use this token later to authorize your self because all endpoints are authorized***

### Tenants (Admin only)
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/tenants | Create a new tenant |
| GET | /api/tenants/{id} | Get tenant by ID |

### Users (requires X-Tenant-Id header)
| Method | Endpoint | Role | Description |
|--------|----------|------|-------------|
| GET | /api/user | Admin + Member | Get all users |
| GET | /api/user/{id} | Admin + Member | Get user by ID |
| POST | /api/user | Admin | Create user |
| PUT | /api/user/{id} | Admin | Update user |
| DELETE | /api/user/{id} | Admin | Soft-delete user |



## Architecture Decisions

- **ITenantContext** — Interface defined in Core, implemented in API layer.
  Middleware resolves `X-Tenant-Id` header and injects TenantId into the scoped
  instance. Services consume it without knowing anything about HTTP.

- **Global Query Filters** — every User query is automatically scoped to the
  current tenant by EF Core. No manual `.Where(u => u.TenantId == ...)` needed through injected ITenantContext interface.


## Trade-offs

- No password hashing — login uses userId for simplicity
- No refresh tokens — JWT expires after 60 minutes
- No pagination on user listing to avoid heavy queries
- Hangfire uses same SQL Server database as the application
