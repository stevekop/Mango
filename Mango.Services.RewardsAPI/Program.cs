using Microsoft.EntityFrameworkCore;
using Mango.Services.RewardsAPI.Data;
using Mango.Services.RewardsAPI.Services;
using Mango.Services.RewardsAPI.Messaging;
using Mango.Services.RewardsAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var optionbuilder = new DbContextOptionsBuilder<AppDBContext>();
optionbuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(new RewardService(optionbuilder.Options));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseAzureServiceBusConsumer();
app.Run();
