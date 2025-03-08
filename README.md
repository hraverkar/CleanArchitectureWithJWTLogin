
# Clean Architecture with JWT Login

This project demonstrates a Clean Architecture implementation in a C# application with JWT (JSON Web Token) as the authentication mechanism.

## Features

- Clean Architecture principles
- JWT Authentication
- Dependency Injection
- Repository Pattern
- Unit Testing

## Getting Started

### Prerequisites

- .NET 6 SDK
- Docker (optional, for containerized deployment)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/hraverkar/CleanArchitectureWithJWTLogin.git
   cd CleanArchitectureWithJWTLogin
   ```

2. Restore the dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

### Running the Application

To run the application locally, use the following command:
```bash
dotnet run
```

### Running with Docker

1. Build the Docker image:
   ```bash
   docker build -t clean-architecture-jwt-login .
   ```

2. Run the Docker container:
   ```bash
   docker run -p 5000:80 clean-architecture-jwt-login
   ```

### Usage

The application exposes the following endpoints:

- `POST /api/auth/login`: Authenticates a user and returns a JWT token.
  
### Project Structure

The project follows the Clean Architecture principles:

- **Core**: Contains the business logic and domain entities.
- **Application**: Contains use cases and application-specific logic.
- **Infrastructure**: Contains implementations for repositories, services, and other infrastructure concerns.
- **API**: Contains the Web API controllers and configuration.

### Authentication

This project uses JWT for authentication. To authenticate, send a POST request to the `/api/auth/login` endpoint with the user's credentials. The response will include a JWT token, which should be included in the `Authorization` header of subsequent requests.

### Contributing
Harshal 

### License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
