using WebAPI.Data.Config;
using WebAPI.Data.DbContext;
using WebAPI.Domain.Interfaces;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;
using Microsoft.Extensions.Hosting;
using WebAPI.Data.Repositories;


public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuration
        builder.Configuration.Sources.Clear();

        IHostEnvironment env = builder.Environment;

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

        AppConfiguration options = new();

        builder.Configuration.GetSection(nameof(AppConfiguration)).Bind(options);

        builder.Services.AddSingleton<IAppConfiguration>(options);

        //DB
        builder.Services.AddSingleton<Dbms>();
        builder.Services.AddSingleton<IUVRepository, UVRepository>();

        builder.Services.AddSingleton<IAppTimer, AppTimer>();
        builder.Services.AddSingleton<IMQTTBroker, MQTTBroker>();
        builder.Services.AddSingleton<ISubscription, Subscription>();
        builder.Services.AddSingleton<IAuthClient, AuthClient>();


        builder.Services.AddSingleton<ICollectDataFromUV, CollectDataFromUV>();
        builder.Services.AddHostedService<CollectDataFromUVFacade>();

        builder.Services.AddOpenApi(options =>
        {
            // Specify the OpenAPI version to use
            options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;

        });



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            app.MapOpenApi();
        }


        app.MapGet("/", () => "Hello, Welcome to FishGuard AI!");


        app.Run();
    }
}