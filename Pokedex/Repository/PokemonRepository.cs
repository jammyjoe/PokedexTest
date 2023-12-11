using Microsoft.EntityFrameworkCore;
using Pokedex.Models;
using Pokedex.RepositoryInterface;

namespace Pokedex.Repository
{
	public class PokemonRepository : IPokemonRepository
	{
		private readonly PokedexContext _context;

		public PokemonRepository(PokedexContext context)
		{
			_context = context;
		}

		public async Task<ICollection<Pokemon>> GetPokemons()
		{
			return await _context.Pokemons.OrderBy(p => p.Id).ToListAsync();
		}
		public async Task<Pokemon> GetPokemon(int id)
		{
			return await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<bool> CreatePokemon(Pokemon pokemon)
		{
			_context.Add(pokemon);
			return await SavePokemon();
		}

		public async Task<bool> DeletePokemon(Pokemon pokemon)
		{
			_context.Remove(pokemon);
			return await SavePokemon();
		}

		public async Task<bool> UpdatePokemon(Pokemon pokemon)
		{
			_context.Update(pokemon);
			return await SavePokemon();
		}

		public async Task<bool> PokemonExists(int id)
		{
			return await _context.Pokemons.AnyAsync(p => p.Id == id);
		}

		public async Task<bool> SavePokemon()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
