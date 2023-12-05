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

		public ICollection<Pokemon> GetPokemons()
		{
			return _context.Pokemons.OrderBy(p => p.Id).ToList();
		}
		public Pokemon GetPokemon(int id)
		{
			return _context.Pokemons.FirstOrDefault(p => p.Id == id);
		}

		public bool CreatePokemon(Pokemon pokemon)
		{
			_context.Add(pokemon);
			return SavePokemon();
		}

		public bool DeletePokemon(Pokemon pokemon)
		{
			_context.Remove(pokemon);
			return SavePokemon();
		}

		public bool UpdatePokemon(Pokemon pokemon)
		{
			_context.Update(pokemon);
			return SavePokemon();
		}

		public bool PokemonExists(int id)
		{
			return _context.Pokemons.Any(p => p.Id == id);
		}

		public bool SavePokemon()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
