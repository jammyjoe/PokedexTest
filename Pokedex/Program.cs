using Microsoft.EntityFrameworkCore;
using Pokedex.Repository;
using Pokedex.RepositoryInterface;
using PokedexAPI.Models;
using PokedexAPI.Repository;
using PokedexAPI.RepositoryInterface;
using AutoMapper;

namespace Pokedex
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
			builder.Services.AddRazorPages();
			builder.Services.AddResponseCaching(x => x.MaximumBodySize = 1024);
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
            builder.Services.AddScoped<ITypeRepository, TypeRepository>();


            builder.Services.AddDbContext<PokedexContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseDefaultFiles();

            app.UseStaticFiles();

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseResponseCaching();

            app.MapRazorPages();

			app.MapControllers();

			app.Run();
		}
	}
}