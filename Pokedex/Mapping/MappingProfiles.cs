using AutoMapper;
using Pokedex.DTOs;
using PokedexAPI.Models;

namespace Pokedex.Mapping
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles() 
		{
			CreateMap<Pokemon, PokemonDto>();
			CreateMap<PokemonDto, Pokemon>();
			CreateMap<Pokemon, PokemonType>();
            CreateMap<PokemonType, Pokemon>();
            CreateMap<PokemonDto, PokemonType>();
            CreateMap<PokemonType, PokemonDto>();


        }
    }
}
