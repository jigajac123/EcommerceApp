using Microsoft.EntityFrameworkCore;
using RestFullApi.Authentication;
using RestFullApi.DataBase;
using RestFullApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices(builder.Services);
builder.Services.AddDbContext<DataBaseContext>(options=> options.UseSqlServer(
    builder.Configuration.GetConnectionString("dataConnect")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ApiKeyAuthFilter>();

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

app.Run();
void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<IProductServices, ProductServices>();
    services.AddTransient<IOrderServices, OrderServices>();
}