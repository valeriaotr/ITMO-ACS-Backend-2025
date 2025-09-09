using ApplicationsService.Clients;
using ApplicationsService.Data;
using ApplicationsService.Repository;
using ApplicationsService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var profilesUrl = builder.Configuration["Services:Profiles"];
var jobsUrl     = builder.Configuration["Services:Jobs"];

profilesUrl ??= "http://localhost:8002";
jobsUrl     ??= "http://localhost:8003";

builder.Services.AddHttpClient<IProfilesClient, ProfilesClient>(c =>
{
    c.BaseAddress = new Uri(profilesUrl);
});
builder.Services.AddHttpClient<IJobsClient, JobsClient>(c =>
{
    c.BaseAddress = new Uri(jobsUrl);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddScoped<IApplicationsRepository, ApplicationsRepository>();
builder.Services.AddScoped<IApplicationsService, ApplicationsService.Services.ApplicationsService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();