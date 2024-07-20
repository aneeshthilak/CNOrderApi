using CNOrderApi.Data;
using CNOrderApi.Models;
using CNOrderApi.Interfaces;
using CNOrderApi.Repositories;
using CNOrderApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IDapperRepository, DapperRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Middleware to validate authenticated user - assume if authenticated user in header user email will be there
//app.UseMiddleware<AuthenticationValidator>();

app.UseAuthorization();



app.MapControllers();

app.Run();
