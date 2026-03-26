-------Contest Management API----------

A minimal API-based backend system built using **ASP.NET Core (.NET 9)** for managing contests, users, leaderboards, and authentication using **ASP.NET Identity**.

---

------Features---------

* User Registration & Login (ASP.NET Identity)
* Role-based access (Normal / VIP users)
* Contest Management
* Leaderboard System
* Rate Limiting
* Global Exception Handling Middleware
* Serilog Logging
* Entity Framework Core (Code First)
* Swagger API Documentation

---

------Tech Stack----------

* **Backend**: ASP.NET Core Minimal API (.NET 9)
* **ORM**: Entity Framework Core
* **Database**: SQL Server
* **Authentication**: ASP.NET Core Identity
* **Logging**: Serilog
* **API Docs**: Swagger (Swashbuckle)

---

--------Project Structure-----------

```
Contest_Management/
│
├── API
│   ├── Interfaces
│   ├── Middleware
│
├── Services
│
├── Models
│
├── Repository (DbContext)
│
├── Seeders
│
└── Program.cs
```

---

---------Architecture------------

The project follows a clean layered structure:

```
Endpoint (Program.cs)
        ↓
Service Layer
        ↓
Repository Layer
        ↓
Database (EF Core)
```

---

------------Authentication & Identity------------

* Uses **ASP.NET Core Identity**
* Users are stored in `AspNetUsers`
* Email is used as username
* Supports roles:

  * `Normal`
  * `VIP`

-------Important:----------------------

* Identity uses `string` as primary key (`Id`)
* All foreign keys referencing users must also be `string`

---------------------------------------

-----------Contest Module---------------

### Contest Entity

* Name
* Description
* Access Level (Normal / VIP)
* Start Time
* End Time
* Prize

### Access Rules

* **VIP Contest** → Only VIP users
* **Normal Contest** → All authenticated users

---

## Leaderboard

* Tracks user scores per contest
* Linked to users via `UserId` (string FK)

---

## Data Seeding

Initial contests are seeded on application startup.

Seeder runs automatically after migrations:

```csharp
await ContestSeeder.SeedAsync(dbContext);
```

---

## Setup Instructions

### 1. Clone the repository

```
git clone <your-repo-url>
```

---

### 2. Run Project

Update `appsettings.json`: If DB name needs to be different

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=CMS;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;"
}
```

---

### 3. Run Migrations

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### 4. Run the Application

```
dotnet run
```

---

### 5. Swagger UI

```
https://localhost:<port>/swagger
```

---

## API Endpoints

### Base

* `GET /` → Health check

---

### Auth

* `POST /registeruser`
* `POST /login`

---

### Contests

* `GET /contests`

---


---

##  Best Practices Followed

* Dependency Injection
* Interface-based design
* Separation of concerns
* Async/await usage
* Centralized error handling

---

## Future Improvements

* JWT Authentication
* Pagination & Filtering
* Contest Questions Module
* Scoring Engine
* Admin Dashboard
* Caching (Redis)

---

## Author

Rahul Hanchate

---

## License

This project is for learning/demo purposes.
