namespace PokedexAPI.DTOs;

public class PokemonResistanceDto
{
    public int Id { get; set; }
    public int? PokemonId { get; set; }
    public PokemonTypeDto Type { get; set; }
}