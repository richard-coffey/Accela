# Accela

Instructions: Use Visual Studio 2019 to run the solution. The Web API and BlazorUI projects need to be both running in order for the app to work correctly. The path to the database in the Connection string is assuming a path of C://Acella/…

Technologies: .NET Core 3.1, Entity Framework Core, Web API, Blazor, LocalDb, XUnit, Postman.

General Notes: I started with creating a model class to represent the single table I would require in the database, in this case a Person class with defined properties (name, email, phone etc…) After this a Code First migration was performed to create the database.

As Entity Framework was used, I needed to make a DbContext class to perform database operations. Fluent API was used in the DbContext class to specific the primary key in the newly created table and what properties were to be required and the maximum length they should be.

For the frontend, I used Blazor, a new technology from Microsoft to allow the use of C# code on the frontend to create dynamic UI’s without the need for Javascript. One example of this in the app, is the use of C# code for client-side validation on the Add Person form instead of using Javascript.

The Web API service was then injected into the Startup.cs file in the Blazor project to allow it access to the CRUD controller methods I had made on the api service (add, edit delete, get all etc…)

Testing: A separate text project was used to test the Web API methods. I used XUnit for this. I then created some dummy data to be inserted into the database and then removed after tests were completed. Postman was also used to verify that Web API calls.
