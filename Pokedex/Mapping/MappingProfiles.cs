using AutoMapper;
using Pokedex.DTOs;
using PokedexAPI.DTOs;
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

            CreateMap<Pokemon, PokemonDto>()
                .ForMember(dest => dest.Type1, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Type2, opt => opt.MapFrom(src => src.Type2));
            CreateMap<PokemonDto, PokemonType>();
            CreateMap<PokemonType, PokemonDto>();
			CreateMap<PokemonType, PokemonTypeDto>();
			CreateMap<PokemonTypeDto, PokemonType>();
        }
    }
}
