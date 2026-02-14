using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Repository;
using BibliotecaAPI.Services;
using BibliotecaAPI.Services.Interfaces;
using BibliotecaAPI.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configurar el contexto de la base de datos con MariaDB
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MariaDbServerVersion(new Version(10, 4, 32))
        ));

// Inyectar los servicios
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILoanService, LoanService>();

// Inyectar los repositorios
builder.Services.AddScoped<IRepository<Users>, UserRepository>(); 
builder.Services.AddScoped<IRepository<Books>, BookRepository>();
builder.Services.AddScoped<IRepository<Loans>, LoanRepository>();

// Inyectar los validators
builder.Services.AddScoped<IValidator<UserDto>, UserInsertValidator>();
builder.Services.AddScoped<IValidator<LoanDto>, LoanInsertValidator>();
builder.Services.AddScoped<IValidator<BookDto>, BookInsertValidator>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
