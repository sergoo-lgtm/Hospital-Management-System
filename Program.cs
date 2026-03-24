using HospitalManagementSystemAPIVersion.Service;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystemAPIVersion.Model;
using HospitalManagementSystemAPIVersion.UnitOfWork;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlServer("Server=localhost,1433;Database=HospitalManagementSystemAPIDB;User Id=sa;Password=Yosuef@2026;TrustServerCertificate=True;"));

builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<PrescriptionService>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();