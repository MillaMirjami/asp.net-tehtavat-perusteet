using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
public class Program {
    public static void Main(string[] args) {
        Console.WriteLine("Starting!");

        // CreateBuilder creates a builder (a factory)
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();

        // builder.Build() creates a web app
        var webApi = builder.Build();

        // once we have the web api, we can start it
        webApi.MapControllers();
        webApi.Run();

        Console.Write("Ending...");
    }
}
