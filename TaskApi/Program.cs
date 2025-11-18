using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using TaskApi.Data;
using TaskApi.Repositories;
using TaskApi.Repositories.Employee_Repositories;
using TaskApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext using InMemory database
builder.Services.AddDbContext<TaskApi.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TasksDb")));

// Register repository and service with DI container
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();