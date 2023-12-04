using Pokedex.Models;

namespace Pokedex.RepositoryInterface
{
	public interface IPokemonRepository
	{
		ICollection<Pokemon> GetPokemons();
		Pokemon GetPokemon(int id);

		bool CreatePokemon (Pokemon pokemon);

		bool UpdatePokemon(Pokemon pokemon);

		bool DeletePokemon(Pokemon pokemon);

		bool SavePokemon();

	}
}
