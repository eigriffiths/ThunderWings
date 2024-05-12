using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ThunderWings.Api.Helpers;
using ThunderWings.Core.Mappers.AutoMapper;
using ThunderWings.Repo.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// AutoMapper
var mappingConfig = new MapperConfiguration(mappingConfig =>
{
    mappingConfig.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite(@"Data Source=thunderwings.db"));

// register types
builder.Services.RegisterTypes();

builder.Services.AddHttpContextAccessor();

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

// if the database does not exist, create it and seed it with data
app.MigrateDatabase();
app.SeedAircraftData();

app.UseSession();

app.Run();
