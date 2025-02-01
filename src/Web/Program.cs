using FluentValidation;
using OrderManagementSystem.Application;
using OrderManagementSystem.Data;
using OrderManagementSystem.Web;
using OrderManagementSystem.Web.Accounting;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureIdentity();

builder.Services.AddContext();
builder.Services.AddUnitOfWork();

builder.Services.AddBusinessServices();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(IValidatorMarker));
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

app.UseHttpsRedirection();

await app.InitializeRolesAsync();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();