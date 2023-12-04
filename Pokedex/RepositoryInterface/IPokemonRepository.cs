using Pokedex.Models;

namespace Pokedex.RepositoryInterface
{
	public interface IPokemonRepository
	{
		ICollection<Pokemon> GetPokemons();
		Pokemon GetPokemon(int id);

		Pokemon CreateCharacter(Pokemon pokemon);

		Pokemon UpdatePokemon(Pokemon pokemon);

		bool DeletePokemon(Pokemon pokemon);

		bool SavePokemon();

	}
}
