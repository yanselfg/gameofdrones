## Game Of Drones. A little game to demostrate how to integrate Angular & .NET Core

A modular, loosely-coupled web application, with SOLID principles, built using Angular 6 and ASP.NET Core 2.1.The following provides a simple layout of a web application organized by the principles defined in the Clean architecture:

* Core project: Holds the Interfaces, Entities and the actual Services.
* DAL: Contains implementations related to data access, such as: Entity Framework DbContext (Custom DbContext), Entity Mappings and classes following the Repository pattern.
* UI project - Contains the Controllers, Views, ViewModels etc., all of which must not interact with Infrastructure directly, but strictly through interfaces defined in the Core layer. In practice this means that we will not have any instantiation of types defined in Infrastructure (DI)

## Technology stack

* ASP.NET Core 2.1
* Angular 6
* Entity Framework Core 2.0 (Code First)
* Sql Server 2017

## Running the application

* Clone the repository or download the .ZIP to a local folder.
* Open the solution file in Visual Studio.
* Open comand prompt inside "GameOfDrones.Web/ClientApp" folder and type: "npm install" to restore all node modules.
* Change the DB connection string inside the "appsettings.json" file.
* Build the entire solution.
* Run the application.
