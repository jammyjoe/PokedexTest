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

		//public Pokemon CreatePokemon(Pokemon pokemon)
		//{

		//}

		public bool DeletePokemon(Pokemon pokemon)
		{
			_context.Remove(pokemon);
			return SavePokemon();
		}

		//public Pokemon UpdatePokemon(Pokemon pokemon)
		//{
		//}
		public bool SavePokemon()
		{
		}
	}
}
