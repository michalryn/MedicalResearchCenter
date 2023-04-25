# MedicalResearchCenter
API for managing a Medical Research Center

## Table of contents
* [Used technologies](#used-technologies)
* [Features](#features)
* [Usage](#usage)

## Used technologies
* NET 6.0
* C#
* Swagger
* Entity Framework Core 7.0.5
* Packages: X.Paged.List.Mvc.Core

## Features
### Patient
* Creating/Reading/Updating/Deleting patients and their details
* Loading all patients (paging)
* Loading patients assigned and not assigned to concrete research project

### Research projects
* Creating/Reading/Updating/Deleting research projects
* Loading all research projects (paging)
* Assigning/Removing patients from a research project

### Lab tests
* Creating/Reading/Updating/Deleting lab tests
* Loading all research lab tests (paging)

### Lab referrals
* Creating/Reading/Deleting lab referrals for a patient
* Confirming patients consent for participation in the lab tests

### Patient tests
* Updating/Deleting patients tests
* Results are read through lab referrals

## Usage
To run the project you need to install the .NET 6 SDK and MSSQL Server. 
### Windows: 
1. Install tools:
- [MSSQL](https://www.microsoft.com/pl-pl/sql-server/sql-server-downloads)
- [SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks)
2. Create a database and put its "ConnectionString" inside the `appsettings.json`, migrations are applied to the database when you first run the project
3. To run the project use Visual Studio
4. The application should be available at `https://localhost:7043` and `http://localhost:5014`
5. Project comes with swagger available at `/swagger/index.html` endpoint

### Linux:
1. Install tools:
- [SDK installation guide](https://learn.microsoft.com/en-us/dotnet/core/install/linux)
- [MSSQL installation guide](https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-overview?view=sql-server-ver16)
2. Create a database and put its "ConnectionString" inside the `appsettings.json`, migrations are applied to the database when you first run the project
3. To run the project use the .NET CLI it should come with the .NET SDK installed in the first step.
- Navigate to the projects repository and use `dotnet build` to build the project ([documentetnion](https://learn.microsoft.com/pl-pl/dotnet/core/tools/dotnet-build))
- Then run the project using `dotnet run` ([documentation](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run))
4. The application should be available at `https://localhost:7043` and `http://localhost:5014`
5. Project comes with swagger available at `/swagger/index.html` endpoint
