# VenueAPI
Simple CRUD API using ASP.NET Core 
## Overview
The application allowes you to preform CRUD operation over:
* Venues
* Customers
* Reservations
-----------------------
## Getting started 

1. Clone this repository
2. Run Migrations by running `dotnet ef database update`in the project directory
3. Run the app by running `dotnet run`

Alternatively you can use visual studio
## The API
You can follow the [Documentation](https://documenter.getpostman.com/view/6342144/RznFpJ24)


### Venues:
* ID       
* Name 
* Address 
* Capacity
* Type
* Privacy

### Customer:
* ID
* Name
* Email

### Reservation
* ID
* VenueID
* CustomerID
* EventName
* Day
* NumberOfPeople
where each reservation takes one day and no 2 reservations can occure at the same day in the same venue
