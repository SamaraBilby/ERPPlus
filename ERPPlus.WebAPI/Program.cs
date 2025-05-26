using ERPPlus.Application.DependencyInjection;
using ERPPlus.Application.Services;
using ERPPlus.Application.Services.Interfaces;
using ERPPlus.Application.Validators;
using ERPPlus.Domain.Interfaces.Repositories;
using ERPPlus.Infrastructure.Context;
using ERPPlus.Infrastructure.DependencyInjection;
using ERPPlus.WebAPI.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

//Repository
builder.Services.AddInfrastructure(builder.Configuration);

//Service
builder.Services.AddApplication();

builder.Services.AddControllers();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<CreateCustomerDTOValidator>()
    .AddValidatorsFromAssemblyContaining<UpdateCustomerDTOValidator>()
    .AddValidatorsFromAssemblyContaining<CreateProductDTOValidatorcs>()
    .AddValidatorsFromAssemblyContaining<UpdateProductDTOValidatorcs>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddERPPlusDbContext(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

