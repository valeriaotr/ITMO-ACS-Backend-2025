using Microsoft.EntityFrameworkCore;
using ProfilesService.Data;
using ProfilesService.Repository;
using ProfilesService.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
builder.Services.AddScoped<IProfilesService, ProfilesService.Service.ProfilesService>();
var app = builder.Build();
app.UseSwagger(); 
app.UseSwaggerUI();
app.MapControllers();
app.Run();