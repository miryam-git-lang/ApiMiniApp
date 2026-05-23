using ApiMiniApp.Data;
using ApiMiniApp.Dtos;
using ApiMiniApp.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddAutoMapper(cfg=> cfg.AddMaps(typeof(Program).Assembly));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<EventCreateDto>, EventCreateDtoValidator>();
builder.Services.AddScoped<IValidator<OrganizerCreateDto>, OrganizerCreateDtoValidator>();
builder.Services.AddScoped<IValidator<TicketCreateDto>, TicketCreateDtoValidator>();
builder.Services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequiredLength = 6;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
}).AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedRolesAsync(roleManager);
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();  

app.Run();
