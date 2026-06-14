# Unit Conversion Platform

A full-stack Unit Conversion Platform built using **ASP.NET Core 8 Web API** and **React + TypeScript** following **Clean Architecture** principles.

## Features

* Convert values between different units
* Length conversions
* Weight conversions
* Temperature conversions
* Global exception handling
* Request validation using FluentValidation
* Swagger/OpenAPI documentation
* Health check endpoint
* Unit tests with xUnit
* Clean Architecture implementation
* Strategy Pattern for conversion logic

---

# Technology Stack

## Backend

* ASP.NET Core 8 Web API
* C#
* FluentValidation
* Swagger / OpenAPI
* xUnit

## Frontend

* React
* TypeScript
* Vite
* Axios

---

# Project Structure

```text
unit-conversion-platform
│
├── backend
│   ├── src
│   │   ├── UnitConversion.Api
│   │   ├── UnitConversion.Application
│   │   ├── UnitConversion.Domain
│   │   └── UnitConversion.Infrastructure
│   │
│   └── tests
│       └── UnitConversion.Tests
│
├── frontend
│
└── README.md
```

## Clean Architecture Layers

### Domain

Contains:

* Entities
* Models
* Interfaces
* Enums
* Exceptions

No dependency on any other layer.

### Application

Contains:

* DTOs
* Services
* Validators
* Business logic

Depends only on Domain.

### Infrastructure

Contains:

* Conversion strategies
* Unit registry
* External implementations

Depends on Application and Domain.

### API

Contains:

* Controllers
* Middleware
* Configuration
* Dependency Injection

Acts as the entry point of the application.

---

# Supported Units

## Length

| Unit      | Symbol |
| --------- | ------ |
| Meter     | m      |
| Kilometer | km     |
| Foot      | ft     |
| Inch      | in     |

## Weight

| Unit     | Symbol |
| -------- | ------ |
| Kilogram | kg     |
| Gram     | g      |
| Pound    | lb     |
| Ounce    | oz     |

## Temperature

| Unit       |
| ---------- |
| Celsius    |
| Fahrenheit |
| Kelvin     |

---

# API Endpoints

## Convert Units

### Request

```http
POST /api/conversions
```

Request Body

```json
{
  "value": 100,
  "fromUnit": "celsius",
  "toUnit": "fahrenheit"
}
```

Response

```json
{
  "originalValue": 100,
  "fromUnit": "celsius",
  "toUnit": "fahrenheit",
  "convertedValue": 212
}
```

---

## Get Supported Units

### Request

```http
GET /api/conversions/supported-units
```

Response

```json
[
  "meter",
  "kilometer",
  "foot",
  "inch",
  "kilogram",
  "gram",
  "pound",
  "ounce",
  "celsius",
  "fahrenheit",
  "kelvin"
]
```

---

## Health Check

### Request

```http
GET /health
```

Response

```json
{
  "status": "Healthy"
}
```

---

# Error Handling

Invalid request:

```json
{
  "message": "Unsupported unit: banana"
}
```

Returns:

```http
400 Bad Request
```

Unexpected server errors return:

```http
500 Internal Server Error
```

with a standardized error response.

---

# Running the Backend

Navigate to:

```bash
cd backend/src/UnitConversion.Api
```

Run:

```bash
dotnet run
```

Swagger UI:

```text
http://localhost:5205/swagger
```

---

# Running the Frontend

Navigate to:

```bash
cd frontend
```

Install dependencies:

```bash
npm install
```

Run application:

```bash
npm run dev
```

Open:

```text
http://localhost:5173
```

---

# Running Tests

Navigate to:

```bash
cd backend/tests/UnitConversion.Tests
```

Run:

```bash
dotnet test
```

Expected result:

```text
Passed: 54
Failed: 0
Skipped: 0
```

The test suite covers:

* Conversion strategies
* Application services
* Validation logic
* Controller endpoints
* Unit registry
* Error scenarios

---

# Design Patterns Used

## Strategy Pattern

Each conversion category is implemented as an independent strategy:

* LengthConversionStrategy
* WeightConversionStrategy
* TemperatureConversionStrategy

This allows new conversion categories to be added without modifying existing logic.

## Dependency Injection

Services and strategies are registered through ASP.NET Core Dependency Injection.

---

# Future Enhancements

* Volume conversions
* Area conversions
* Currency conversions
* Docker containerization
* CI/CD pipeline using GitHub Actions
* Database-backed unit configuration

---

# Author

Abhijeet M Baloji

* GitHub: https://github.com/abhijeetbaloji
* LinkedIn: https://linkedin.com/in/abhijeet-baloji
