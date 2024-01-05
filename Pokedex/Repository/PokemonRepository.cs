using Microsoft.EntityFrameworkCore;
using Pokedex.DTOs;
using Pokedex.RepositoryInterface;
using PokedexAPI.Models;

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

        public async Task<Pokemon> GetPokemon(string name)
        {
            return await _context.Pokemons.FirstOrDefaultAsync(p => p.Name == name);
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

        public async Task<bool> PokemonExists(string name)
        {
            return await _context.Pokemons.AnyAsync(p => p.Name == name);
        }
        public async Task<bool> SavePokemon()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
