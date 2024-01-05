using PokedexAPI.DTOs;

namespace Pokedex.DTOs
{
	public record PokemonDto
	{
		public int Id { get; set; }
		public string Name { get; set; } 
		public PokemonTypeDto Type1 { get; set; } 
		public PokemonTypeDto? Type2 { get; set; }
		//public PokemonWeakness Weakness { get; set; }
		//public PokemonResistance Resistance { get; set; }
	}
}
