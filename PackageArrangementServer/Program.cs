using Microsoft.EntityFrameworkCore;
using PackageArrangementServer.Data;
using PackageArrangementServer.Models;
using PackageArrangementServer.Models.DeliveryProperties;
using PackageArrangementServer.Services;
using PackageArrangementServer.Services.ResultServices;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


ServicePointManager
    .ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;

builder.Services.AddDbContext<APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIContext") ?? throw new InvalidOperationException("Connection string 'APIContext' not found.")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeliveryService, DeliveryServiceDB>();
builder.Services.AddScoped<IDeliveryServiceHelper, DeliveryServiceHelper>();
builder.Services.AddScoped<IContainerService, ContainerService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IRabbitMqProducerService, RabbitMqProducerService>();
builder.Services.AddScoped<IRabbitMqConsumerService, RabbitMqConsumerService>();
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.Configure<DeliveriesDatabase>(
    builder.Configuration.GetSection("DeliveriesDatabase"));

//builder.Services.AddSingleton<DeliveryService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow All",
        builder =>
        {
            builder
            .WithOrigins("http://localhost:7165")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
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

app.MapControllers()

app.Run();
