using PackageArrangementServer.Models.DeliveryProperties;
using PackageArrangementServer.Services;
using PackageArrangementServer.Services.ResultServices;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


ServicePointManager
    .ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeliveryService, DeliveryServiceDB>();
builder.Services.AddScoped<IDeliveryServiceHelper, DeliveryServiceHelper>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IRabbitMqProducerService, RabbitMqProducerService>();
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
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

var app = builder.Build();
app.UseCors("Allow All");


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
