using PokedexAPI.Models;

namespace Pokedex.DTOs
{
	public record PokemonDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public PokemonType Type1 { get; set; } = null!;
		public PokemonType Type2 { get; set; }
		public PokemonWeakness Weakness { get; set; }
		public PokemonResistance Resistance { get; set; }
	}
}
