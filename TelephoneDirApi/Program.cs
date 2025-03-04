using System.Data.SqlClient;
using System.Data;
using TelephoneDirApi.DAL.GenderRepository;
using TelephoneDirApi.DAL.CountryRepository;
using TelephoneDirApi.DAL.StatesRepository;
using TelephoneDirApi.DAL.CityRepostirory;
using TelephoneDirApi.DAL.PersonsRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddScoped<GenderRepository>();
builder.Services.AddScoped<CountryRepository>();
builder.Services.AddScoped<StateRepository>();
builder.Services.AddScoped<CityRepository>();
builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
