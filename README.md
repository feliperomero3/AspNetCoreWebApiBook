# ASP.NET Core Web API

Based on the Web Application built in the Book 'Building Web APIs with ASP.NET Core' by Valerio De Sanctis (Manning, 2023).

## Prerequisites

- Visual Studio 2022 (17.9.6)
- .NET 8.0.204 SDK

## Getting started

1. Clone this repository.
1. Open the solution file.
1. Build the solution.
1. Press F5 to launch the project in debug mode.
1. Open a terminal at the root of the repository.
1. Change to the `src\MyBoardGameList` directory.
1. Execute `dotnet user-jwts create --role Administrator` to create a bearer token.
1. Use the bearer token to authenticate requests to the API.

**Note:** The first time you launch the project the database `MyBoardGameList` will be created and seeded with data from the `bgg_dataset_test.csv` file. 
The database is created automatically using EF Core's `Database.EnsureCreated()`.

## License

	MIT License

	Copyright (c) 2024 Felipe Romero

	Permission is hereby granted, free of charge, to any person obtaining a copy
	of this software and associated documentation files (the "Software"), to deal
	in the Software without restriction, including without limitation the rights
	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the Software is
	furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in all
	copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
	SOFTWARE.

	Creative Commons Attribution 4.0 International Public License
	
	Dilini Samarasinghe, July 5, 2021, "BoardGameGeek Dataset on Board Games", IEEE Dataport, doi: https://dx.doi.org/10.21227/9g61-bs59.

