# Hospital Management System API

A professional ASP.NET Core Web API for managing core hospital operations, including patients, doctors, appointments, payments, prescriptions, dashboard metrics, and appointment reporting.

## Overview

This project is built as a layered backend application using:

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- SMTP email notifications
- Repository and Unit of Work patterns

The API supports the main workflows of a small hospital or clinic system:

- Register and manage patients
- Register and manage doctors
- Schedule appointments
- Create and update payments
- Create and maintain prescriptions
- Expose dashboard summary counts
- Return appointment details from a database view
- Send email confirmation when an appointment is created

## Project Structure

```text
Controllers/        API endpoints
Service/            Business logic
Repository/         Generic repository implementation
UnitOfWork/         Transaction and repository coordination
Model/              EF Core entities and DbContext
DTO/                Request/response contracts
Proxy/              Email service integration
CustomExceptions/   Global exception handling
Migrations/         EF Core database migrations
```

## Technology Stack

- .NET 10 (`net10.0`)
- ASP.NET Core Web API
- Entity Framework Core 10
- SQL Server provider for EF Core
- Swashbuckle Swagger UI
- MailKit package reference is included, while SMTP sending is currently implemented with `System.Net.Mail`

## Features

### Patients

- Add a patient
- Get patient by ID
- Delete patient
- List patients with pagination and optional name search

### Doctors

- Add a doctor
- Get doctor by ID
- Delete doctor
- List doctors with pagination, name filtering, and specialization filtering

### Appointments

- Create an appointment
- Get appointment by ID
- Delete appointment
- List appointments with pagination
- Filter appointments by status and doctor
- Complete an appointment workflow
- Automatically create a related payment when an appointment is created
- Send appointment confirmation email to the patient when an email address is available

### Payments

- Mark payment as paid
- Update payment amount
- Get payment by ID
- List payments with pagination

### Prescriptions

- Create prescription for an appointment
- Update prescription notes
- Update prescription medications
- Delete prescription
- Get prescription by ID
- List prescriptions with pagination and optional search

### Dashboard and Reporting

- Dashboard counts endpoint backed by a SQL stored procedure
- Appointment details endpoint backed by a SQL view

## Architecture

The solution follows a layered architecture:

1. `Controllers` handle HTTP requests and responses.
2. `Service` classes implement business rules.
3. `Repository` and `UnitOfWork` abstract data access.
4. `Model` contains the EF Core entities and database mappings.
5. `DTO` classes define the API contracts.

This separation keeps the API organized, easier to test, and easier to extend.

## Prerequisites

Before running the project, make sure you have:

- .NET 10 SDK installed
- SQL Server running locally or remotely
- A valid SQL Server connection string
- SMTP credentials if you want appointment emails to be sent

## Configuration

Application settings are stored in `appsettings.json`.

Current configuration sections:

- `ConnectionStrings:myconnection`
- `EmailSettings:Email`
- `EmailSettings:Password`
- `EmailSettings:Host`
- `EmailSettings:Port`
- `EmailSettings:EnableSsl`

Example structure:

```json
{
  "ConnectionStrings": {
    "myconnection": "Server=localhost,1433;Database=HospitalSystemNew;User Id=sa;Password=your_password;TrustServerCertificate=True;"
  },
  "EmailSettings": {
    "Email": "your-email@example.com",
    "Password": "your-app-password",
    "Host": "smtp.gmail.com",
    "Port": 587,
    "EnableSsl": true
  }
}
```

## Security Note

Sensitive values should not be committed to source control. For production or shared development environments, move database credentials and email credentials to:

- environment variables
- `dotnet user-secrets`
- secure secret management services

## Database Setup

The project uses Entity Framework Core migrations and also depends on additional SQL objects:

- View: `vw_AppointmentDetails`
- Stored procedure: `GetDashboardCounts`

Make sure these objects exist in the target database, otherwise the related endpoints will fail.

Apply migrations with:

```bash
dotnet ef database update
```

If needed, create a new migration with:

```bash
dotnet ef migrations add InitialCreate
```

## Seed Data

The `HospitalDbContext` seeds sample data for:

- Patients
- Doctors
- Appointments
- Payments
- Prescriptions

This is useful for local testing and Swagger exploration.

## Running the Application

Restore packages and run the API:

```bash
dotnet restore
dotnet run
```

By default, the project launches on:

- `http://localhost:5024`

Swagger UI is available in development at:

- `http://localhost:5024/swagger`

## API Endpoints

### Patient

- `POST /api/Patient/add`
- `GET /api/Patient/get/{id}`
- `DELETE /api/Patient/delete/{id}`
- `GET /api/Patient/list`

### Doctor

- `POST /api/Doctor/add`
- `GET /api/Doctor/get/{id}`
- `DELETE /api/Doctor/delete/{id}`
- `GET /api/Doctor/list`

### Appointment

- `POST /api/Appointment/add`
- `PUT /api/Appointment/{id}/complete`
- `GET /api/Appointment/get/{id}`
- `DELETE /api/Appointment/delete/{id}`
- `GET /api/Appointment/list`

### Payment

- `POST /api/Payment/pay`
- `PUT /api/Payment/update-amount`
- `GET /api/Payment/get/{id}`
- `GET /api/Payment/list`

### Prescription

- `POST /api/Prescription/add`
- `PUT /api/Prescription/update-notes`
- `PUT /api/Prescription/update-medications`
- `DELETE /api/Prescription/delete/{id}`
- `GET /api/Prescription/get/{id}`
- `GET /api/Prescription/list`

### Dashboard and Reports

- `GET /api/Dashboard/counts`
- `GET /api/AppointmentView/details`

## Development Notes

- Swagger is enabled only in the development environment.
- Global exception handling is configured through a custom exception handler.
- Pagination is implemented through a reusable `PagedResult<T>` model and LINQ extension helpers.
- The API currently uses SQL Server through `UseSqlServer`.

## Suggested Improvements

For production readiness, consider adding:

- Authentication and authorization
- Input validation with FluentValidation or data annotations
- Structured logging
- Automated tests
- Docker support
- CI/CD pipeline
- Environment-specific configuration management

## License

This project is intended for educational or internal development use unless a separate license is provided.
