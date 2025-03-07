# ambev-developer-evaluation
API for sales management using DDD, including CRUD operations and event logging

## Features

- **Domain-Driven Design (DDD):** Clean architecture with separation of concerns.
- **CRUD Operations:** Full support for creating, reading, updating, and deleting sales records.
- **Event Logging:** Tracks and logs important events for auditing and debugging.
- **Docker Support:** Easy setup and deployment using Docker.

## Prerequisites

Before running the application, ensure you have the following installed:

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Installation

1. Clone the repository:
   ```shell
   git clone https://github.com/esteveslean/ambev-developer-evaluation.git
   ```

2. Update the .env.sample file with your desired configuration.

3. Start the application using Docker Compose:
    ```shell
    docker compose -f .\docker-compose.yml up -d
    ```

### API Documentation

Once the application is running, you can access the API documentation (if available) at:

- Swagger UI: http://localhost:5000/swagger
- Or any other documentation tool you've configured.
   
### Contribuição

1. Fork the project.
2. Create a new branch for your feature or bugfix:  (`git checkout -b YourBranchName`)
3. Commit your changes: (`git commit -m 'feat: add some feature'`)
4. Push your branch to GitHub: (`git push origin YourBranchName`)
5. Open a Pull Request and describe your changes.
