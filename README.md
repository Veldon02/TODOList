README file for TODO List API
=============================

This repository contains a .NET Core Web API for managing a TODO list. This README file provides information on how to set up, run, and test the API.

Getting Started
---------------

1. Install [.NET Core](https://dotnet.microsoft.com/download).
2. Clone this repository.
```
git clone https://github.com/<username>/todo-list-api.git
```
3. Open the command-line interface and navigate to the project root directory.
```
cd todo-list-api
```
4. Build the project.
```
dotnet build
```

Running the API
---------------

You can run the API using the following command:
```
dotnet run --project <project-name>.csproj
```
where `<project-name>` is the name of the project containing the `Startup.cs` file.

By default, the API will run and listen for HTTP requests on `http://localhost:5000` and `https://localhost:5001`.

API Endpoints
-------------

The API currently supports the following endpoints:

### GET api/TodoItems

Returns a list of all TODO items.

### GET api/TodoItems/{id}

Returns the TODO item with the specified `id`.

### POST api/TodoItems

Creates a new TODO item.

### PUT api/TodoItems/{id}

Updates the TODO item with the specified `id`.

### DELETE api/TodoItems/{id}

Deletes the TODO item with the specified `id`.

Testing the API
---------------

The API is tested using the Xunit testing framework. You can run the tests using the following command:
```
dotnet test
```

Dependencies
---------------

This API depends on the following packages:

- Microsoft.AspNetCore.Mvc
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.InMemory
- Microsoft.Extensions.DependencyInjection

Contributing
------------

Contributions to this repository are welcome. Fork this repository and make changes locally. Once finished, create a pull request to merge your changes into the `main` branch.

License
-------

This repository is licensed under the MIT license. See `LICENSE` for details.