using Microsoft.EntityFrameworkCore;
using Authorisation.Models;

var builder = WebApplication.CreateBuilder(args); 
// Creates webApplicationBuilder object, easy way to define and 
// configure ASP.NET Core application.


// ADD SERVICES TO THE CONTAINER. //
            //
builder.Services.AddControllers(); // Adds (MVC = Model-View-Controllers) controllers to use
            //
builder.Services.AddSwaggerGen(); // Registers Swagger-services, 
                                  // UI that displays API-documentation and 
                                  // enables API-requests directly on browser
            //
builder.Services.AddDbContext<UserContext> // Adds DbContext-service to 
                                           // dependency injection (DI) container so 
                                           // UserContext-class can be injected to 
                                           // other classes (ex. to controllers) 
                                           // during program.
                                           // UserContext represents connection to the database 
                                           // and enables access to the db tables.
            //
(opt => opt.UseInMemoryDatabase("UserList")); 
// opt is randomly chosen name for DbContextOptionsBuilder-class which helps to define db's configuration, such as type and connection settings.
// Configures that In-Memory db is being used, name ''UserList'' is symbolic, 
// since data is not permanently stored.

builder.Services.AddOpenApi(); // Registers services which creates and offers OpenAPI-doc for the app.
                               // Enables API-doc to be in JSON- or YAML-format.
                               // Prepares API's doc to be in the OpenAPI Spesification standard format. (OAS)

var app = builder.Build(); // Build gathers all the previously defined configurations 
                           // and creates a webApplicationBuilder object with those configs 
                           // and is ready to accepts HTTP-requests.

if (app.Environment.IsDevelopment()) // Configure the HTTP request pipeline. (Configure Swagger UI)
{
    app.UseSwagger(); // Creates Swagger to the documentation
    app.UseSwaggerUI(); // Creates UI for the Swagger documentation
    app.UseDeveloperExeptionPage(); // Displays error messages & stack race info in development
}

app.UseHttpsRedirection(); // Middleware, helps redirect if 
                          // user tries http-request into https-request 
                          // (Hypertext Transfer Protocol Secure).

app.UseAuthentication(); // Middleware, checks identity
                         // Then authorization checks which access can this identity have.

app.UseAuthorization(); // Middleware, ensures that authorization protocols 
                        // (roles, rigths, access) are checked before user can
                        // access secured recourses or functions. 

app.MapControllers(); // Configures the routing and HTTP-requests,
                      // Declares that HTTP-requests are routed to controllers.

app.Run(); // Starts the application

//  FOR MINIMAL APIS THAT DON'T USE CONTROLLERS //
                //
// builder.Services.AddEndpointsApiExplorer(); //
                //
// Searches programs API's endpoints (API-routes) 
// and helps to create doc of it for the Swagger doc. 
                //
// app.MapGet("/hello", () => "Hello, World!"); // Declares API-routes
                //
                //
                //
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi