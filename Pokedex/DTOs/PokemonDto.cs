namespace Pokedex.DTOs
{
	public record PokemonDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Type1 { get; set; } = null!;
		public string? Type2 { get; set; }
	}
}
