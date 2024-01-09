namespace PokedexAPI.DTOs;

public class PokemonWeaknessDto
{
    public int Id { get; set; }
    public int? PokemonId { get; set; }
    public PokemonTypeDto Type { get; set; }
}
