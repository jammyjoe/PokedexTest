using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PokedexAPI.Repository;
using PokedexAPI.RepositoryInterface;
using PokedexAPI.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddResponseCaching(options => options.MaximumBodySize = 1024);
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddDbContext<PokedexContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:7132", "http://localhost:5187")
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pokedex API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokedex API v1"));
}

app.UseCors(); // Use CORS before routing

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching();

app.MapRazorPages();
app.MapControllers();

app.Run();