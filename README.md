# Boomtown Notes App by Drew M. Forsberg

## Prerequisites
The following items must be installed:

1. Visual Studio 2022
2. .NET 6
3. npm (https://www.npmjs.com/), which is included with Node.js
4. Vue CLI (https://cli.vuejs.org/)

## Project setup

1. Using the command line or Powershell, go to the ClientApp directory (your path will vary but should be something like  "..[your source code folder]\Boomtown.Notes\Boomtown.Notes.WebApp\ClientApp")
2. Install all client side packages by running `npm install`
3. In Visual Studio 2022, open the Package Manager Console window (Tools > NuGet Package Manager > Package Manager Console)
4. Run the command `Update-Database` to create the LocalDB database that the app will use to save notes data.

## Running the project

1. Confirm that the solution builds
2. Use F5 to run the single page web app in debug mode (or Ctrl + F5 to run without debugging)
