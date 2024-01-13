using DataAccessLayer.DataContext;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Contracts;
using BusinessLayer.Services.TaskService;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Services.EmailService;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
builder.Services.AddTransient(typeof(ITaskRepository<>), typeof(TaskRepository<>));
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IEmailService, EmailService>();


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

app.Run();