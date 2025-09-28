# TooliRent - Tool Rental Management System

A comprehensive RESTful API for managing tool rentals, built with ASP.NET Core 8.0 following clean architecture principles.

## Project Overview

TooliRent is a tool rental management system that allows users to browse, reserve, and rent tools. The system supports two user roles:
- **Members**: Can browse tools, create reservations, and manage their own orders
- **Admins**: Full CRUD operations on all resources and user management

## Architecture

The project follows **N-tier/Clean Architecture** with clear separation of concerns:
```

# TooliRent/
# ├── TooliRent/                   # Presentation layer (API)
- │   ├── Controllers/             # API Controllers
- │   └── Validators/              # FluentValidation
# ├── TooliRent.Core/              # Domain layer
- │   ├── Models/                  # Entity models
- │   ├── DTOs/                    # Data Transfer Objects
- │   └── Interfaces/              # Service & Repository interfaces
# ├── TooliRent.Infrastructure/    # Data access layer
- │   ├── Data/                    # DbContext
- │   └── Repositories/            # Repository implementations
# └── TooliRent.Services/          # Business logic layer
      ├── Service/                 # Service implementations
      └── Auth/                    # JWT token service
      └── Mapping/                 # AutoMapper profiles

```

### Design Patterns Used
- **Repository Pattern**: Data access abstraction
- **Service Pattern**: Business logic encapsulation
- **Dependency Injection**: Loose coupling between layers
- **DTO Pattern**: Data transfer and validation

### Key Technologies
- **ASP.NET Core 8.0** - Web API framework
- **Entity Framework Core 9.0** - ORM
- **SQL Server** - Database
- **AutoMapper** - Object mapping
- **JWT Bearer Authentication** - Security
- **Swagger/OpenAPI** - API documentation
- **FluentValidation** - Input validation

## Database Schema

### Main Entities
- **User**: User accounts with roles (Admin/Member)
- **Category**: Tool categories
- **Tool**: Available tools for rent
- **Reservation**: Tool reservations
- **ReservationTool**: Junction table for reservation-tool relationship
- **OrderDetails**: Checkout and return tracking

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone <your-repo-url>
   cd TooliRent
   ```

2. **Update connection string**
   
   Edit `appsettings.json` in the TooliRent project:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ToolIRentDB;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   dotnet ef database update --project TooliRent.Infrastructure --startup-project TooliRent
   ```

4. **Run the application**
   ```bash
   dotnet run --project TooliRent
   ```

5. **Access Swagger UI**
   
   Navigate to: `https://localhost:7044/swagger` (port may vary)

### JWT Configuration

The API uses JWT for authentication. Default configuration in `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "your-super-secret-key-here-make-it-at-least-32-characters-long",
    "Issuer": "TooliRent",
    "Audience": "TooliRent-Users",
    "ExpireMinutes": 120
  }
}
```

## API Documentation

### Authentication Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Register new user | No |
| POST | `/api/auth/login` | Login and get JWT token | No |
| GET | `/api/auth/profile/{userId}` | Get user profile | Yes |
| PUT | `/api/auth/profile/{userId}` | Update user profile | Yes |
| POST | `/api/auth/change-password/{userId}` | Change password | Yes |
| POST | `/api/auth/forgot-password` | Request password reset | No |
| PUT | `/api/auth/admin/update-user-status/{userId}` | Update user role (Admin) | Admin |

### Category Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/categories` | Get all categories | No |
| GET | `/api/categories/{id}` | Get category by ID | No |
| POST | `/api/categories` | Create category | Admin |
| PUT | `/api/categories/{id}` | Update category | Admin |
| DELETE | `/api/categories/{id}` | Delete category | Admin |

### Tool Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/tool` | Get all tools | No |
| GET | `/api/tool/available` | Get available tools | No |
| GET | `/api/tool/{id}` | Get tool by ID | No |
| GET | `/api/tool/category/{categoryId}` | Get tools by category | No |
| GET | `/api/tool/select` | Get tools for dropdown | No |
| POST | `/api/tool` | Create tool | Admin |
| PUT | `/api/tool/{id}` | Update tool | Admin |
| PATCH | `/api/tool/{id}/status` | Update tool status | Admin |
| DELETE | `/api/tool/{id}` | Delete tool | Admin |

### Reservation Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/reservation` | Get all reservations | Admin |
| GET | `/api/reservation/active` | Get active reservations | Admin |
| GET | `/api/reservation/{id}` | Get reservation by ID | Yes |
| GET | `/api/reservation/user/{userId}` | Get user's reservations | Yes |
| POST | `/api/reservation/user/{userId}` | Create reservation | Yes |
| PUT | `/api/reservation/{id}` | Update reservation | Yes |
| PATCH | `/api/reservation/{id}/cancel` | Cancel reservation | Yes |
| POST | `/api/reservation/check-availability` | Check tool availability | Yes |
| DELETE | `/api/reservation/{id}` | Delete reservation | Admin |

### Order Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/orderdetails` | Get all orders | Admin |
| GET | `/api/orderdetails/active` | Get active orders | Admin |
| GET | `/api/orderdetails/overdue` | Get overdue orders | Admin |
| GET | `/api/orderdetails/{id}` | Get order by ID | Admin |
| GET | `/api/orderdetails/user/{userId}` | Get user's orders | Yes |
| POST | `/api/orderdetails/user/{userId}` | Create order | Yes |
| PUT | `/api/orderdetails/{id}` | Update order | Yes |
| PATCH | `/api/orderdetails/{id}/checkout` | Check out order | Yes |
| PATCH | `/api/orderdetails/{id}/return` | Return order | Yes |
| PATCH | `/api/orderdetails/{id}/cancel` | Cancel order | Yes |
| GET | `/api/orderdetails/{id}/late-fee` | Calculate late fee | Yes |
| DELETE | `/api/orderdetails/{id}` | Delete order | Admin |

## Data Models

### User
```json
{
  "id": 1,
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "role": "Member"
}
```

### Tool
```json
{
  "id": 1,
  "name": "Cordless Drill",
  "brand": "Bosch",
  "model": "GSR 18V",
  "status": "Available",
  "description": "Professional cordless drill",
  "pricePerDay": 150.00,
  "isAvailable": true,
  "serialNumber": "SN12345",
  "categoryId": 1,
  "categoryName": "Power Tools"
}
```

### Reservation
```json
{
  "id": 1,
  "date2Hire": "2025-10-01T08:00:00",
  "date2Return": "2025-10-05T17:00:00",
  "status": "Active",
  "totalTools": 2,
  "estimatedTotalPrice": 600.00,
  "userName": "John Doe"
}
```

### Order
```json
{
  "id": 1,
  "date2Hire": "2025-10-01T08:00:00",
  "date2Return": "2025-10-05T17:00:00",
  "status": "CheckedOut",
  "totalPrice": 600.00,
  "lateFee": 0.00,
  "checkedOutAt": "2025-10-01T09:30:00",
  "toolName": "Cordless Drill",
  "userName": "John Doe"
}
```

## Authentication Flow

1. **Register**: POST to `/api/auth/register` with user details
2. **Login**: POST to `/api/auth/login` to receive JWT token
3. **Use Token**: Include token in Authorization header: `Bearer <your-token>`
4. **Access Protected Routes**: Token is validated automatically

### Example Login Request
```json
{
  "email": "john.doe@example.com",
  "password": "password123"
}
```

### Example Login Response
```json
{
  "success": true,
  "message": "Login successful",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expires": "2025-09-27T14:00:00Z",
  "user": {
    "id": 1,
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "role": "Member"
  }
}
```

## Business Rules

### Reservations
- Reservations must be created at least 1 day in advance
- Return date must be after hire date
- Tools must be available for the requested period
- Members can only view/modify their own reservations
- Admins can view/modify all reservations

### Orders
- Orders are created from reservations or directly
- Checkout marks tools as picked up
- Return calculates any late fees (50% of daily rate per day late)
- Members can only view their own orders
- Only admins can perform checkout/return operations

### Tool Availability
- Tools marked as unavailable cannot be reserved
- Reserved tools are checked for conflicts with existing reservations
- System validates date ranges before creating reservations

## Error Handling

The API returns standard HTTP status codes:
- **200 OK**: Successful GET/PUT/PATCH
- **201 Created**: Successful POST
- **204 No Content**: Successful DELETE
- **400 Bad Request**: Validation errors
- **401 Unauthorized**: Missing/invalid token
- **403 Forbidden**: Insufficient permissions
- **404 Not Found**: Resource not found
- **409 Conflict**: Business rule violation
- **500 Internal Server Error**: Server error

## Development Notes

### Seed Data
The database includes seed data for testing:
- 4 Categories (Power Tools, Hand Tools, Measuring Tools, Safety Equipment)
- 3 Users (1 Admin, 2 Members)
- Sample tools in each category

## Finishing Notes

This is a school project for Systemutvecklare.NET, class SUT24. 
### PS. My Teacher loves Star Trek
