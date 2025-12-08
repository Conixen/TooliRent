# TooliRent - Tool Rental Management System

A comprehensive RESTful API for managing tool rentals, built with ASP.NET Core 8.0 following clean architecture principles.

## Project Overview

TooliRent is a tool rental management system that allows users to browse and rent tools. The system supports two user roles:
- **Members**: Can browse tools, create orders, and manage their own rentals
- **Admins**: Full CRUD operations on all resources, user management, and rental statistics

## Architecture

The project follows **N-tier/Clean Architecture** with clear separation of concerns:
```
TooliRent/
├── TooliRent/                   # Presentation layer (API)
│   ├── Controllers/             # API Controllers
│   └── Validators/              # FluentValidation
├── TooliRent.Core/              # Domain layer
│   ├── Models/                  # Entity models
│   ├── DTOs/                    # Data Transfer Objects
│   └── Interfaces/              # Service & Repository interfaces
├── TooliRent.Infrastructure/    # Data access layer
│   ├── Data/                    # DbContext
│   └── Repositories/            # Repository implementations
└── TooliRent.Services/          # Business logic layer
    ├── Service/                 # Service implementations
    ├── Auth/                    # JWT token service
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
- **BCrypt.Net** - Password hashing
- **Swagger/OpenAPI** - API documentation
- **FluentValidation** - Input validation

## Database Schema

### Main Entities
- **User**: User accounts with roles (Admin/Member) and active status
- **Category**: Tool categories
- **Tool**: Available tools for rent with availability status
- **OrderDetails**: Complete rental tracking (booking, checkout, return)

### Entity Relationships
```
User (1) ─────< (M) OrderDetails
Category (1) ─────< (M) Tool
Tool (1) ─────< (M) OrderDetails
```

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Installation Steps

1. **Clone the repository**
```bash
   git clone 
   cd TooliRent
```

2. **Install required NuGet packages**
```bash
   dotnet add package BCrypt.Net-Next
```

3. **Update connection string**
   
   Edit `appsettings.json` in the TooliRent project:
```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YourDB;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
```

4. **Apply database migrations**
```bash
   dotnet ef migrations add InitialCreate --project TooliRent.Infrastructure --startup-project TooliRent
   dotnet ef database update --project TooliRent.Infrastructure --startup-project TooliRent
```

5. **Run the application**
```bash
   dotnet run --project TooliRent
```

6. **Access Swagger UI**
   
   Navigate to: `https://localhost:7044/swagger` (port may vary)

### JWT Configuration

The API uses JWT for authentication. Configuration in `appsettings.json`:
```json
{
  "Jwt": {
    "Key": "your-super-secret-key-here-make-it-at-least-32-characters-long",
    "Issuer": "TooliRent",
    "Audience": "TooliRent-Users"
  }
}
```

## API Documentation

### Authentication Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Register new user | No |
| POST | `/api/auth/login` | Login and get JWT token | No |
| GET | `/api/auth/profile` | Get current user profile | Yes (Member/Admin) |
| PUT | `/api/auth/profile` | Update current user profile | Yes (Member/Admin) |
| GET | `/api/auth/users` | Get all users | Admin |
| PUT | `/api/auth/users/{id}/status` | Activate/deactivate user | Admin |

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
| GET | `/api/tools` | Get all tools | No |
| GET | `/api/tools/{id}` | Get tool by ID | No |
| GET | `/api/tools/available` | Get available tools only | No |
| GET | `/api/tools/category/{categoryId}` | Get tools by category | No |
| POST | `/api/tools` | Create tool | Admin |
| PUT | `/api/tools/{id}` | Update tool | Admin |
| DELETE | `/api/tools/{id}` | Delete tool | Admin |

### Order Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/orderdetails` | Get all orders | Admin |
| GET | `/api/orderdetails/{id}` | Get order by ID | Yes (Member/Admin) |
| POST | `/api/orderdetails` | Create order (single or multiple tools) | Yes (Member/Admin) |
| PUT | `/api/orderdetails/{id}` | Update order | Admin |
| DELETE | `/api/orderdetails/{id}` | Delete order | Admin |
| POST | `/api/orderdetails/{id}/checkout` | Check out order (mark as picked up) | Admin |
| POST | `/api/orderdetails/{id}/return` | Return order (with late fee calculation) | Admin |
| POST | `/api/orderdetails/{id}/cancel` | Cancel order | Yes (Member/Admin) |

### Statistics Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/statistics` | Get rental statistics | Admin |

## Data Models

### User
```json
{
  "id": 1,
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "role": "Member",
  "isActive": true
}
```

### Tool
```json
{
  "id": 1,
  "name": "Slagborrmaskin",
  "brand": "Bosch",
  "model": "GSB 13 RE",
  "description": "Professionell slagborrmaskin för betong och murning",
  "pricePerDay": 45.00,
  "isAvailable": true,
  "serialNumber": "BOSCH-001",
  "categoryId": 1,
  "categoryName": "Elverktyg"
}
```

### Create Order (Single or Multiple Tools)
```json
{
  "toolIds": [1, 2, 3],
  "date2Hire": "2024-12-01T08:00:00",
  "date2Return": "2024-12-05T17:00:00"
}
```

### Order Response
```json
{
  "message": "3 tools booked successfully: Slagborrmaskin, Vinkelslip, Sticksåg",
  "date2Hire": "2024-12-01T08:00:00",
  "date2Return": "2024-12-05T17:00:00",
  "status": "Pending",
  "tools": [
    {
      "orderId": 1,
      "toolId": 1,
      "toolName": "Slagborrmaskin"
    },
    {
      "orderId": 2,
      "toolId": 2,
      "toolName": "Vinkelslip"
    },
    {
      "orderId": 3,
      "toolId": 3,
      "toolName": "Sticksåg"
    }
  ]
}
```

### Order Details
```json
{
  "id": 1,
  "date2Hire": "2024-12-01T08:00:00",
  "date2Return": "2024-12-05T17:00:00",
  "status": "CheckedOut",
  "totalPrice": 180.00,
  "lateFee": 0.00,
  "checkedOutAt": "2024-12-01T09:30:00",
  "returnedAt": null,
  "createdAt": "2024-11-28T10:00:00",
  "updatedAt": "2024-12-01T09:30:00",
  "userId": 2,
  "toolId": 1,
  "userName": "John Doe",
  "toolName": "Slagborrmaskin"
}
```

### Statistics
```json
{
  "totalUsers": 6,
  "totalTools": 17,
  "totalOrders": 10,
  "activeOrders": 4,
  "completedOrders": 5
}
```

## Authentication Flow

1. **Register**: POST to `/api/auth/register` with user details
2. **Login**: POST to `/api/auth/login` to receive JWT token
3. **Use Token**: Include token in Authorization header: `Bearer <your-token>`
4. **Access Protected Routes**: Token is validated automatically

### Example Register Request
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "password": "SecurePassword123!",
  "role": "Member"
}
```

### Example Login Request
```json
{
  "email": "john.doe@example.com",
  "password": "SecurePassword123!"
}
```

### Example Login Response
```json
{
  "greatSucessVeryNice": true,
  "message": "Login successful",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
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

### Orders
- **Booking**: Users can book one or multiple tools in a single order
- **Date Validation**: Return date must be after hire date, hire date cannot be in the past
- **Availability Check**: Tools must be available and not already booked for the requested period
- **Conflict Detection**: System checks for booking conflicts before creating orders
- **Status Flow**: Pending → CheckedOut → Returned (or Cancelled)
- **Late Fees**: Automatically calculated at 50 kr per day when returned late
- **Permissions**: 
  - Members can create and cancel their own orders
  - Only Admins can checkout, return, update, or delete orders
  - Admins can view all orders, Members can only view their own

### Tool Availability
- Tools marked as `isAvailable: false` cannot be booked
- System validates against existing Pending and CheckedOut orders
- Tools are not blocked by Returned or Cancelled orders

### User Management
- **Password Security**: All passwords are hashed using BCrypt
- **Active Status**: Admins can activate/deactivate user accounts
- **Role-Based Access**: Member and Admin roles with different permissions

## Security Features

### Password Hashing
- Uses BCrypt.Net for secure password hashing
- Passwords are never stored in plain text
- Automatic verification during login

### JWT Token Authentication
- Tokens expire after 2 hours
- Contains user ID and role claims
- Required for all protected endpoints

### Authorization Levels
- **Public**: Categories, Tools (viewing)
- **Authenticated**: Profile management, Order creation/cancellation
- **Admin Only**: User management, Statistics, Order checkout/return, All CRUD operations

## Error Handling

The API returns standard HTTP status codes:
- **200 OK**: Successful GET/PUT
- **201 Created**: Successful POST
- **204 No Content**: Successful DELETE
- **400 Bad Request**: Validation errors, Business rule violations
- **401 Unauthorized**: Missing/invalid token
- **403 Forbidden**: Insufficient permissions
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Server error

### Example Error Response
```json
{
  "message": "Tool 'Vinkelslip' is already booked from 2024-12-01 to 2024-12-05"
}
```

## Validation

All input is validated using FluentValidation:

### Order Creation
- At least one tool must be selected
- Hire date cannot be in the past
- Return date must be after hire date
- Tools must exist and be available

### User Registration
- Email must be valid and unique
- Password minimum 6 characters
- First/Last name required
- Role must be "Admin" or "Member"

### Tool Creation
- Name, Brand, Model required
- Price must be greater than 0
- Valid category must be selected

## Development Notes

### Seed Data
The database includes comprehensive seed data for testing:

**Users** (6):
- 1 Admin: `Leon.Johanssonsens@example.com` / `Adminpower`
- 4 Active Members
- 1 Inactive Member (for testing user activation)

**Categories** (5):
- Elverktyg, Handverktyg, Mätverktyg, Säkerhetsutrustning, Trädgårdsverktyg

**Tools** (17):
- 5 Elverktyg
- 3 Handverktyg
- 3 Mätverktyg
- 3 Säkerhetsutrustning
- 3 Trädgårdsverktyg
- 1 tool unavailable (for testing)

**Orders** (10):
- 5 Returned (including 1 with late fee)
- 2 CheckedOut (currently rented)
- 2 Pending (upcoming bookings)
- 1 Cancelled

### Testing the API

1. **Login as Admin**:
```json
   POST /api/auth/login
   {
     "email": "Leon.Johanssonsens@example.com",
     "password": "Adminpower"
   }
```

2. **Login as Member**:
```json
   POST /api/auth/login
   {
     "email": "DoeTheJohn@example.com",
     "password": "password123"
   }
```

3. **View Statistics** (Admin only):
```
   GET /api/statistics
```

4. **Book Multiple Tools**:
```json
   POST /api/orderdetails
   {
     "toolIds": [1, 2, 3],
     "date2Hire": "2024-12-10",
     "date2Return": "2024-12-15"
   }
```
*PS. My Teacher loves Star Trek* 

---
