using Application;
using Application.DaoInterfaces;
using Application.DAOs;
using Application.Logic;
using Application.LogicInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRideDao, RideDao>();
builder.Services.AddScoped<IReservationDao, ReservationDao>();
builder.Services.AddScoped<IRideLogic, RideLogic>();
builder.Services.AddScoped<IReservationLogic, ReservationLogic>();
builder.Services.AddScoped<IReservationDao, ReservationDao>();
builder.Services.AddScoped<IDriverLogic, DriverLogic>();
builder.Services.AddScoped<IDriverDao, DriverDao>();
// add addscoped when reservationdao finished by bartek


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();