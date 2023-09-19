# Prerequisites
## Before we begin, please make sure that you have the following installed on your system:

* Visual Studio 2022 or later
* .NET SDK 7 or later
* MySQL Server and MySQL Workbench (or any other MySQL client)
## Step 1: Clone the repository
Clone or download the repository from GitHub to your local machine.

## Step 2: Create the database
Open MySQL Workbench and create a new database named TodoDatabase. This is the database where the TodoItems will be stored.

## Step 3: Configure the database connection
Navigate to the TodoApi/appsettings.json file in the project and replace the {YOUR_DATABASE_URL},{YOUR_DATABASE_NAME}, {YOUR_USERNAME}, and {YOUR_PASSWORD} placeholders with your database url, name, username, and password, respectively.

## Step 4: Open the project in Visual Studio
Open the TodoApi project in Visual Studio by clicking on the TodoApi.sln file.

## Step 5: Run the application
Press F5 to launch the application in debug mode. The TodoApi should start running and listening on port 5001.

## Step 6: Test the application
You can test the application using a tool like Postman or cURL.

* To get all todo items, send a GET request to https://localhost:5001/todo.
* To get a specific todo item, send a GET request to https://localhost:5001/todo/{id} where {id} is the ID of the item.
* To create a new todo item, send a POST request to https://localhost:5001/todo with a JSON body containing the title and description properties.
* To update an existing todo item, send a PUT request to https://localhost:5001/todo/{id} with a JSON body containing the title and description properties.
* To delete a todo item, send a DELETE request to https://localhost:5001/todo/{id} where {id} is the ID of the item.

And that's it! You have successfully built and run the TodoApi application.




