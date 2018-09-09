# dotnet-webapi-jwtauth
Dotnet core 2.1 web API server using JWT authentication with a Postgres DB context

## Getting Started

To get started, the following should do the trick.

```
> git clone https://github.com/bkot88/dotnet-webapi-jwtauth.git
> cd dotnet-webapi-jwtauth/TokenDemo
> dotnet build
> dotnet run
```

Server should listen on https://localhost:5001 at which point you could query with Postman.
POST /api/auth with Basic Auth is required to obtain a token.

### Prerequisites

You need to have a Postgres server running locally. The connection string can be edited in appsettings.json.

```
{
...
  "ConnectionStrings": {
    "Postgres": "Host=localhost;Database=app;Username=postgres;Password=adminadmin"
  },
  "JwtSecretKey": "SecureStringKeyGoesHere",
...
}
```
