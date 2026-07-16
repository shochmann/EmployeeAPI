# EmployeeAPI

## Overview

This project is an ASP.NET Core Web API that uses .NET 10 target framework. Solution was created with Visual Studio 2026.

The API includes:

- GET "https://localhost:7193/getEmployees"
- POST "https://localhost:7193/postEmployee"
- DELETE "https://localhost:7193/deleteEmployee"
- Swagger UI "https://localhost:7193/swagger/index.html"

## Technologies

- .NET 10
- ASP.NET Core Web API
- MSTest
- Moq
- Swagger

## Running

1. Open the solution in Visual Studio 2026.
2. Set the API project as the startup project.
3. Press F5.
4. Browse to: "https://localhost:7193/swagger/index.html"

or

Open a terminal and Navigate to EmployeeAPI/src
run command "dotnet run --launch-profile https"
Browse to: "https://localhost:7193/swagger/index.html"
To shut down API use "CTRL + c" if using the terminal to run

## Running Tests

In Visual Studio Open Test Explorer and run all tests.

or

Open a terminal and Navigate to EmployeeAPI/tests
run command "dotnet test"

Most of the tests I created are in EmployeeControllerTests to show the use of Moq and how the service can be mocked to mimic the dependency injection of when the API runs naturally. I did add another EmployeeServiceTests file that has just a couple of tests in it to show how the actual service and data could be used without a mock.

## Project Structure

Controllers/
Data/
Models/
Services/

## Notes

- Dependencies are injected for the data and the service as Singletons in the Program.cs file.
- Validation for the fields occurs as attributes that are attached to the Employee model.
- Implemented an Interface for my service to decouple the controller from the implementation. (e.g. in future if I wanted to get data from database I would not have to change the controller.)
