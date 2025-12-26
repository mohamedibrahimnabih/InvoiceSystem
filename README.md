# Invoice Management System

A comprehensive invoice management system built with **ASP.NET Core 9** using **Clean Architecture**, **Repository Pattern**, **Mediator Pattern**, and **CQRS**. The system includes a RESTful Web API backend and a Razor Pages frontend with dynamic JavaScript functionality.

## 🚀 Quick Start

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A code editor (Visual Studio 2022, VS Code, or Rider)
- Web browser (Chrome, Edge, or Firefox)

### Getting Started

1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd InvoiceSystem
   ```

2. **Start the Web API Backend**

   Open a terminal and run:
   ```bash
   cd src/InvoiceSystem.Web
   dotnet run
   ```

   The API will start at: **https://localhost:7234**

   You can access Swagger UI at: **https://localhost:7234/swagger**

3. **Start the Razor Pages Frontend** (Open a new terminal)

   In a new terminal window, run:
   ```bash
   cd src/InvoiceSystem.RazorPages
   dotnet run
   ```

   The UI will start at: **https://localhost:7001**

4. **Open Your Browser**

   Navigate to **https://localhost:7001** to access the Invoice Management System UI.

## 📋 Features

### Backend API Features
- ✅ **Full CRUD Operations** for invoices
- ✅ **Repository Pattern** with generic repository and specifications
- ✅ **Mediator Pattern** using MediatR for CQRS implementation
- ✅ **Domain-Driven Design** with aggregates, value objects, and domain events
- ✅ **Clean Architecture** with clear separation of concerns
- ✅ **Entity Framework Core** with SQLite database
- ✅ **RESTful API** with Swagger documentation
- ✅ **Automatic calculations** for totals, taxes, and discounts

### Frontend Features
- ✅ **Responsive UI** built with Bootstrap 5
- ✅ **Dynamic invoice item management** with JavaScript
- ✅ **Real-time calculations** for subtotals, taxes, and totals
- ✅ **AJAX calls** to Web API using Fetch API
- ✅ **Delete confirmation modal**
- ✅ **Success/error notifications**
- ✅ **Professional navigation** with Bootstrap Icons

## 🎯 How to Use

### Creating an Invoice

1. Click **"Create New Invoice"** or navigate to **Invoices → Create**
2. Fill in the invoice details:
   - Invoice Number (e.g., `INV-2025-001`)
   - Customer Name
   - Invoice Date
   - Due Date (optional)
   - Notes (optional)
3. The form starts with **2 items by default**
4. Click **"Add Item"** to add more items
5. For each item, enter:
   - Product Name
   - Quantity
   - Unit Price
   - Tax Rate (%)
   - Discount (%)
   - Description (optional)
6. **Watch the totals calculate in real-time!**
7. Click **"Create Invoice"** to save

### Viewing Invoices

- Navigate to **Invoices** in the navigation menu
- View all invoices in a table with:
  - Invoice Number
  - Date
  - Customer Name
  - Total Amount
  - Status (Draft/Issued/Paid)
  - Due Date

### Deleting an Invoice

1. Click the **red "Delete"** button on any invoice
2. Confirm deletion in the modal
3. Invoice is removed immediately

## 🧪 Testing the Application

### Sample Test Data

Create an invoice with these details:

**Invoice Information:**
```
Invoice Number: INV-2025-001
Customer Name: Acme Corporation
Invoice Date: [Today's Date]
Due Date: [30 days from today]
Notes: First test invoice
```

**Item 1:**
```
Product: Laptop
Quantity: 2
Unit Price: 1000.00
Tax Rate: 10
Discount: 5
```

**Item 2:**
```
Product: Wireless Mouse
Quantity: 5
Unit Price: 25.00
Tax Rate: 10
Discount: 0
```

**Expected Calculations:**
- **Item 1:** (2 × $1000) × 0.95 = $1,900 + 10% tax = $190
- **Item 2:** (5 × $25) = $125 + 10% tax = $12.50
- **Subtotal:** $2,025.00
- **Tax:** $202.50
- **Total:** $2,227.50

### API Endpoints

The API provides the following endpoints:

- `POST /api/invoices` - Create a new invoice
- `GET /api/invoices` - Get all invoices (list view)
- `GET /api/invoices/{id}` - Get invoice by ID with items
- `PUT /api/invoices/{id}` - Update an invoice
- `DELETE /api/invoices/{id}` - Delete an invoice

**Test with Swagger:**
Navigate to **https://localhost:7234/swagger** to test the API directly.

## 🏗️ Project Structure

```
InvoiceSystem/
├── src/
│   ├── InvoiceSystem.Core/              # Domain Layer
│   │   ├── InvoiceAggregate/            # Invoice entities and business logic
│   │   │   ├── Invoice.cs               # Aggregate root
│   │   │   ├── InvoiceItem.cs           # Entity
│   │   │   ├── InvoiceStatus.cs         # SmartEnum (Draft/Issued/Paid)
│   │   │   ├── Events/                  # Domain events
│   │   │   └── Specifications/          # Query specifications
│   │   ├── Interfaces/                  # Repository and service interfaces
│   │   └── Services/                    # Domain services
│   │
│   ├── InvoiceSystem.UseCases/          # Application Layer
│   │   └── Invoices/                    # CQRS Commands and Queries
│   │       ├── Create/                  # CreateInvoiceCommand & Handler
│   │       ├── Get/                     # GetInvoiceQuery & Handler
│   │       ├── List/                    # ListInvoicesQuery & Handler
│   │       ├── Update/                  # UpdateInvoiceCommand & Handler
│   │       └── Delete/                  # DeleteInvoiceCommand & Handler
│   │
│   ├── InvoiceSystem.Infrastructure/    # Infrastructure Layer
│   │   ├── Data/
│   │   │   ├── Config/                  # EF Core entity configurations
│   │   │   ├── Queries/                 # Query services (CQRS read side)
│   │   │   ├── AppDbContext.cs          # Database context
│   │   │   └── EfRepository.cs          # Generic repository
│   │   └── InfrastructureServiceExtensions.cs
│   │
│   ├── InvoiceSystem.Web/               # Web API Project
│   │   ├── Controllers/
│   │   │   └── InvoicesController.cs    # Traditional ASP.NET Core Controller
│   │   └── Models/                      # API request/response models
│   │
│   └── InvoiceSystem.RazorPages/        # Frontend UI Project
│       ├── Pages/
│       │   ├── Index.cshtml             # Home page
│       │   └── Invoices/
│       │       ├── Index.cshtml         # Invoice list page
│       │       └── Create.cshtml        # Create invoice page
│       └── wwwroot/
│           └── js/
│               ├── invoices.js          # List & delete functionality
│               └── create-invoice.js    # Create with dynamic items
│
└── tests/                               # Test projects
```

## 🛠️ Technologies Used

### Backend
- **ASP.NET Core 9** - Web framework
- **Entity Framework Core 9** - ORM
- **MediatR** - Mediator pattern implementation
- **Ardalis.SharedKernel** - DDD building blocks
- **Ardalis.Result** - Result pattern
- **Ardalis.SmartEnum** - Type-safe enumerations
- **Ardalis.Specification** - Repository specification pattern
- **FastEndpoints** - Fast API endpoints (existing in template)
- **SQLite** - Database (Development)

### Frontend
- **ASP.NET Core Razor Pages 9** - Server-side rendering
- **Bootstrap 5** - CSS framework
- **Bootstrap Icons** - Icon library
- **Vanilla JavaScript** - Dynamic UI interactions
- **Fetch API** - HTTP client for API calls

## 🎨 Design Patterns Implemented

1. **Repository Pattern**
   - Generic repository with specification support
   - Separates data access from business logic
   - Located in `Infrastructure/Data/EfRepository.cs`

2. **Mediator Pattern**
   - MediatR for CQRS implementation
   - Decouples request/response handling
   - All commands/queries in `UseCases` project

3. **CQRS (Command Query Responsibility Segregation)**
   - Commands for writes (Create, Update, Delete)
   - Queries for reads (Get, List)
   - Optimized query service for list view

4. **Aggregate Pattern**
   - Invoice as Aggregate Root
   - InvoiceItem as child entity
   - Business logic encapsulated in aggregate

5. **Specification Pattern**
   - Reusable query logic
   - Type-safe query composition
   - Examples: `InvoiceByIdSpec`, `InvoiceByIdWithItemsSpec`

6. **Domain Events**
   - Event-driven architecture
   - Events: `InvoiceCreatedEvent`, `InvoiceUpdatedEvent`, `InvoiceDeletedEvent`

7. **Smart Enum**
   - Type-safe status enumeration
   - `InvoiceStatus`: Draft, Issued, Paid

8. **Result Pattern**
   - Robust error handling
   - Type-safe operation results

## 🔧 Configuration

### API Configuration
The Web API runs on:
- HTTPS: `https://localhost:7234`
- HTTP: `http://localhost:5234`

### Frontend Configuration
The Razor Pages app runs on:
- HTTPS: `https://localhost:7001`
- HTTP: `http://localhost:5001`

To change the API URL in the frontend, update:
```json
// src/InvoiceSystem.RazorPages/appsettings.json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7234"
  }
}
```

Also update the JavaScript files:
- `wwwroot/js/invoices.js` (line 2)
- `wwwroot/js/create-invoice.js` (line 2)

### Database
The application uses SQLite for development. The database is created automatically on first run at:
```
src/InvoiceSystem.Web/database.sqlite
```

To use SQL Server instead:
1. Update connection string in `appsettings.json`
2. Change `UseSqlite()` to `UseSqlServer()` in `InfrastructureServiceExtensions.cs`

## 🧩 OOP Principles Applied

1. **Encapsulation**
   - Private setters with public methods
   - Business logic hidden in entities
   - Example: `Invoice.AddItem()`, `InvoiceItem.CalculateLineTotal()`

2. **Single Responsibility**
   - Each class has one reason to change
   - Controllers handle HTTP, handlers handle business logic
   - Services focused on specific domains

3. **Dependency Inversion**
   - Depend on abstractions (interfaces)
   - `IRepository<T>`, `IMediator`, etc.
   - Dependency injection throughout

4. **Interface Segregation**
   - Small, focused interfaces
   - `IReadRepository<T>` vs `IRepository<T>`

5. **Open/Closed Principle**
   - Specifications allow extension without modification
   - MediatR handlers can be added without changing existing code

## 📝 Additional Notes

### Database Migrations
The application uses `EnsureCreatedAsync()` for automatic database creation. For production, use migrations:

```bash
cd src/InvoiceSystem.Web
dotnet ef migrations add InitialCreate --project ../InvoiceSystem.Infrastructure/InvoiceSystem.Infrastructure.csproj
dotnet ef database update --project ../InvoiceSystem.Infrastructure/InvoiceSystem.Infrastructure.csproj
```

### Building the Solution
```bash
# Build all projects
dotnet build

# Run tests
dotnet test
```

### Running in Production
For production deployment:
1. Change to SQL Server database
2. Update connection strings
3. Configure CORS if needed
4. Enable HTTPS
5. Configure logging and monitoring

## 🤝 Contributing

This project was built as a technical demonstration of Clean Architecture, Repository Pattern, and Mediator Pattern with ASP.NET Core 9.

## 📄 License

This project is based on the [Ardalis Clean Architecture Template](https://github.com/ardalis/CleanArchitecture).

## 👨‍💻 Author

Built with ASP.NET Core 9, demonstrating modern software architecture patterns and best practices.

---

**Happy Coding! 🚀**

For more information about Clean Architecture, visit the [original template repository](https://github.com/ardalis/CleanArchitecture).
