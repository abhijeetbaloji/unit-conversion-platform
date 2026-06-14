# Unit Conversion Platform

ASP.NET Core Web API with a React + TypeScript + Vite frontend (optional).

## Quick start — backend only

```bash
cd backend/src/UnitConversion.Api
dotnet run
```

Swagger UI: http://localhost:5205/swagger

## API endpoints

| Method | Path | Description |
|--------|------|-------------|
| `POST` | `/api/conversions` | Convert a value |
| `GET`  | `/api/conversions/supported-units` | List all supported units |
| `GET`  | `/health` | Health check |

### POST /api/conversions

```json
{
  "value": 100,
  "fromUnit": "celsius",
  "toUnit": "fahrenheit"
}
```

Response:

```json
{
  "originalValue": 100,
  "fromUnit": "celsius",
  "toUnit": "fahrenheit",
  "convertedValue": 212
}
```

Supported categories: **Length** (meter, kilometer, foot, inch), **Weight** (kilogram, gram, pound, ounce), **Temperature** (celsius, fahrenheit, kelvin).

Error response (400):

```json
{ "message": "Unsupported unit: banana" }
```

## Run tests

```bash
cd backend
dotnet test
```

43 tests across 5 test classes.

## Frontend (optional)

```bash
cd frontend
npm install
npm run dev
```

Open http://localhost:5173

The frontend reads `VITE_API_URL` from `frontend/.env` (defaults to `http://localhost:5205`). Change it to match your backend port if needed.

## Architecture

```
backend/
  src/
    UnitConversion.Domain/          # Enums, interfaces, models — no dependencies
    UnitConversion.Application/     # DTOs, services, validators — depends on Domain
    UnitConversion.Infrastructure/  # Strategies, registry — depends on Application
    UnitConversion.Api/             # Controllers, middleware, Program.cs
  tests/
    UnitConversion.Tests/           # xUnit tests

frontend/
  src/
    api.ts       # Axios client (reads VITE_API_URL)
    App.tsx      # Single page — discovers units from GET /supported-units
    App.css      # Plain CSS, no frameworks
```

Clean Architecture: Domain has zero external dependencies. Conversion logic lives in interchangeable `IConversionStrategy` implementations (Strategy pattern). New unit categories can be added without touching the API layer.
