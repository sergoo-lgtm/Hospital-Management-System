using HospitalManagementSystemAPIVersion.Service;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystemAPIVersion.Model;
using HospitalManagementSystemAPIVersion.CustomExceptions;

using HospitalManagementSystemAPIVersion.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"))
);

builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<PrescriptionService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<AppointmentViewService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseAuthorization();
app.MapControllers();
app.Run();