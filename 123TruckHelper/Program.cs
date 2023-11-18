using Microsoft.EntityFrameworkCore;
using _123TruckHelper;
using _123TruckHelper.Controllers;


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

var app = builder.Build();

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

MQTTListener.Handle_Received_Application_Message();
