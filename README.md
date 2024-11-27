# BookYourStay

**BookYourStay** is a web application designed to streamline the process of booking accommodations. Built using a modular and scalable architecture, the project serves as a comprehensive example of implementing modern web development practices.

## Features

- **Accommodation Management**: Add, edit, and manage listings for various accommodations.
- **Booking System**: Enables users to browse and book accommodations with ease.
- **Search and Filter**: Allows filtering by location, price, and availability.
- **Modular Architecture**: Separates application concerns into different layers:
  - **Domain Layer**: Defines core business logic.
  - **Application Layer**: Manages application-specific operations and workflows.
  - **Infrastructure Layer**: Handles data access and external integrations.
  - **Web Layer**: Provides the front-end for user interaction.

## Technologies Used

- **Backend**: ASP.NET Core
- **Database**: SQL Server
- **Dependency Injection**: For better modularity and testability.

## Prerequisites

- Visual Studio 2022 or higher
- .NET Core SDK 6.0 or later

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/BuketCulfaoglu/BookYourStay.git
   ```
2. Navigate to the project directory:
   ```bash
   cd BookYourStay
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Set up the database:
   - Update the connection string in `appsettings.json`.
   - Apply migrations:
     ```bash
     dotnet ef database update
     ```
5. Run the application:
   ```bash
   dotnet run
   ```
6. Open the application in your browser at `http://localhost:5000`.

## Contribution

Contributions are welcome! Feel free to fork the repository and submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgments

Special thanks to all contributors and those who provided support and feedback throughout the development of this project.

