using Pokedex.DTOs;
using PokedexAPI.Models;

namespace Pokedex.RepositoryInterface
{
	public interface IPokemonRepository
	{
		Task<ICollection<Pokemon>> GetPokemons();
        Task<string> GetTypeById(int typeId);
        Task<Pokemon> GetPokemon(int id);
        Task<Pokemon> GetPokemon(string name);
        Task<bool> CreatePokemon(Pokemon pokemon);
		Task<bool> UpdatePokemon(Pokemon pokemon);
		Task<bool> PokemonExists(int id);
        Task<bool> PokemonExists(string name);
        Task<bool> DeletePokemon(Pokemon pokemon);
		Task<bool> SavePokemon();
    }
}
