# PenaltyCalculationApp
This project is an Angular application that calculates penalties for overdue book borrowings based on the borrowing date, return date, and the selected country's configuration.

## Features

- Calculate penalties for overdue book borrowings based on the selected country's weekend configuration.
- Input checkout date and return date to calculate penalties.
- Select the country to apply specific weekend configurations.
- Display the calculated penalty for overdue books.
- Handle errors and provide user feedback for form submissions.

## Technologies Used

- Angular 
- Angular Reactive Forms
- Angular HttpClient for API communication
- HTML5 and CSS3 for UI design
- TypeScript
- C# with .NET 8
- Entity Framework Core (EF Core) 
- Design patterns and best practices 

## Installation
1. Install the dependencies: `npm install`

## Usage

1. Run the development server: `ng serve`
2. Open the application in your browser at `http://localhost:4200/`
3. Open the Swagger in your browser at `https://localhost:7295/swagger/index.html

## Configuration

To configure the application, you can update the API endpoint in the `penalty-calculation.service.ts` file with your backend server's URL.

## Database 

Two tables:
1- BookBorrowings include the below columns:
Id: primary key – type integer 
BookTitle: type nvarchar(500)
BorrowerName: type nvarchar(500)
BorrowingDate: type datetime 
DueDate: type datetime 
CountryId: type integer this filed is the Id of the country from Countries table

2- Countries include the below columns:
Id: primary key – type integer 
CountryName: type nvarchar(500)
WeekendConfiguration: type nvarchar(500)




![image](https://github.com/AbdoBr/Penalty-CalculationApp/assets/148472248/6ad48ebe-e434-474b-8700-1f5db8de7def)
