using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todo.Models;
using Serilog;


// Create configurationbuilder and logger configuration

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.Build();

Log.Logger = new LoggerConfiguration()
.ReadFrom.Configuration(configuration)
.CreateLogger();
///////////////////////////////////////////////////////

Log.Information("Logger creation successful");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Brings information about controllers, inside of this code it's possible to manupulate the requests before they're sent to the client.
app.Use(async (context, next) =>
{
    // Log the incoming request method and URL
    Log.Information("Request Method: {Method} | Request Path: {Path}", context.Request.Method, context.Request.Path);

    // Capture the original response body stream
    var originalResponseBodyStream = context.Response.Body;

    // Create a new memory stream to capture the response body
    using (var responseMemoryStream = new MemoryStream())
    {
        // Replace the response body with our memory stream
        context.Response.Body = responseMemoryStream;

        try
        {
            // Call the next middleware (this processes the request and generates the response)
            await next.Invoke();

            // After the response is generated, capture the response body
            responseMemoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(responseMemoryStream).ReadToEndAsync();

            if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 500)
            {
                Log.Error("Response Status Code: {StatusCode}",
                context.Response.StatusCode);
            }
            else
            {
                // Log the response status code and body (if available)
                Log.Information("Response Status Code: {StatusCode}",
                context.Response.StatusCode);
            }
        // Copy the memory stream back to the original response body so the client gets it
            responseMemoryStream.Seek(0, SeekOrigin.Begin);
            await responseMemoryStream.CopyToAsync(originalResponseBodyStream);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during the request processing
            Log.Error(ex, "An error occurred while processing the request.");

            // Set the response status code to 500 if an exception occurred
            context.Response.StatusCode = 500;
            var errorResponse = new { message = "An error occurred while processing your request." };

            // Write the error response back to the client
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
});
////////////////////////////////////////////////////////////////////////////////////////////////////////
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

