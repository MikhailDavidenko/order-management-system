using FluentValidation;
using Microsoft.AspNetCore.Identity;
using OrderManagementSystem.Application;
using OrderManagementSystem.Data;
using OrderManagementSystem.Data.Common;
using OrderManagementSystem.Domain;
using OrderManagementSystem.Web;
using OrderManagementSystem.Web.Accounting;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorization();

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>();/*
    .AddApiEndpoints();*/

builder.Services.AddContext();
builder.Services.AddUnitOfWork();

builder.Services.AddBusinessServices();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(IValidatorMarker));
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

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
//app.MapIdentityApi<ApplicationUser>();

app.Run();