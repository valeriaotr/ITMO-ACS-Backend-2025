using JobsService.Clients;
using JobsService.Data;
using JobsService.Repository;
using JobsService.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var profilesUrl = builder.Configuration["Services:Profiles"] ?? "http://localhost:8002";
var searchUrl   = builder.Configuration["Services:Search"]   ?? "http://localhost:8005";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobsService, JobsService.Service.JobsService>();
builder.Services.AddHttpClient<ISearchClient, SearchClient>(c =>
{
    c.BaseAddress = new Uri(searchUrl);
});

builder.Services.AddHttpClient<IProfilesClient, ProfilesClient>(c =>
{
    c.BaseAddress = new Uri(profilesUrl);
});

// HttpClient -> Search
builder.Services.AddHttpClient<ISearchClient, SearchClient>(c =>
{
    c.BaseAddress = new Uri(searchUrl);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();