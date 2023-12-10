using AutoMapper;
using Pokedex.DTOs;
using Pokedex.Models;

namespace Pokedex.Mapping
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles() 
		{
			CreateMap<Pokemon, PokemonDto>() ;
			CreateMap<PokemonDto, Pokemon>();
		}
	}
}
