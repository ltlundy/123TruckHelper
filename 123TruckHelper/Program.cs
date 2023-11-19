using _123TruckHelper.Ingestion;
using _123TruckHelper.Models.EF;
using _123TruckHelper.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF
builder.Services.AddScoped<TruckHelperDbContext>();

// not sure if we need both of these, but leaving it for now
builder.Services.AddDbContext<TruckHelperDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
});

// DI
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IDataIngestionService, DataIngestionService>();
builder.Services.AddTransient<ITruckService, TruckService>();
builder.Services.AddTransient<ILoadService, LoadService>();

var app = builder.Build();

// Enable CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

// Remove HTTPS redirection
// app.UseHttpsRedirection();

// Remove SSL certificate validation
// ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

app.UseAuthorization();

app.MapControllers();

// MQTT listener
MQTTListener.InitializeServiceProvider(app.Services);
MQTTListener.ListenAndProcessAsync();

// Automatic notification sender
NotificationManager.InitializeServiceProvider(app.Services);
NotificationManager.Manage();

app.Run();
