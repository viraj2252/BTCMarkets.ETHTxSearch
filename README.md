
# BTCMarkets.ETHTxSearch

A simple application to demonstrate the power of **Clean Architecture** and how different responsibilities are segregated in different projects to make each component as decoupled as possible. The goal is to demonstrate simple Ethereum network scan to get transaction details. the solution use structure that can be used to build Domain-Driven Design (DDD)-based or simply well-factored, SOLID applications using .NET Core.

# Design Decisions and Dependencies
The whole solution is build using Microsoft technology stack (.NET Core 3.1, ASP.NET Core 3.1). below is a list of the technology dependencies this includes

## The Core Project
The core project is the center of Clean Architecture, and all other project should point towards this. This includes things like

 - Entities
 - Interfaces
 - Event Handlers

 Idea is to publish this project as Nuget package and shared between multiple projects. The API invocation is generalized in a way you can easily integrate this project to call any external API and convert the response to required result set.

## The Infrastructure Project
Most of the application's external dependencies are implemented in this. This implements interfaces defined in Core project. In our case, this is where all services reside. Access to external Infura API happens here. All Infura environments are defined here.

## The Web Project
Is the entry point to the application through ASP.NET Core web API. Client application using React/Redux also bundled as part of web application. Build process take care of building API and Client app. All dependencies are defined in this project. Logging as dependency also defined here. All DTOs are defined here for external transport.

## The Test Projects
XUnit is used as the testing framework along with Moq to mock InfuraAPIService. Due to my limited time I have only defined Unit Testing project to demo the test cases. Integration tests are missing due to same reason.  Simply change the network of Infura to test net and run the integration test to see the scenarios are passing.

# Installation / Run
This solution depends on .NET Core 3.1 run time and NodeJS. Simply run the Web project and you are good to go.