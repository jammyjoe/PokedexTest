using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokedex.DTOs;
using Pokedex.Models;
using Pokedex.Repository;
using Pokedex.RepositoryInterface;

namespace Pokedex.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PokemonController : ControllerBase
	{
		private readonly PokedexContext _context;
		private readonly IPokemonRepository _pokemonRepository;
		private readonly IMapper _mapper;

		public PokemonController(PokedexContext context, IPokemonRepository pokemonRepository, IMapper mapper)
		{
			_context = context;
			_pokemonRepository = pokemonRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public ActionResult<Pokemon> GetPokemons()
		{
			var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

			if (!ModelState.IsValid)
				return NoContent();

			return Ok(pokemons);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]

		public ActionResult<Pokemon> GetPokemon(int id)
		{
			if (!_pokemonRepository.PokemonExists(id))
				return NotFound("This pokemon does not exist");

			var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(id));

			if (!ModelState.IsValid)
				return NoContent();

			return Ok(pokemon);
		}

		[HttpPost("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public ActionResult<Pokemon> CreatePokemon(int id,
			[FromBody] Pokemon pokemonCreate)
		{
			if (pokemonCreate == null)
				return BadRequest("This Id is invalid");

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

			if(!_pokemonRepository.CreatePokemon(pokemonMap));
			{
				ModelState.AddModelError("", "Something went wrong while savin");
				return StatusCode(500, ModelState);
			}

			return Ok("Succesfully Created");
		}


		[HttpPut("{id}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public ActionResult<Pokemon> UpdatePokemon(int id,
	[FromBody] Pokemon pokemonUpdate)
		{
			if (pokemonUpdate == null)
				return BadRequest("This Id is invalid");

			if (!_pokemonRepository.PokemonExists(id))
				return NotFound("This pokemon does not exist");

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var pokemonMap = _mapper.Map<Pokemon>(pokemonUpdate);

			if(!_pokemonRepository.UpdatePokemon(pokemonMap));
			ModelState.AddModelError("","Something went wrong while saving");
			return StatusCode(500, ModelState);


			return NoContent();
		}


		[HttpDelete("{id}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public ActionResult<Pokemon> DeletePokemon(int id)
		{
			if (!_pokemonRepository.PokemonExists(id))
				return NotFound("This pokemon does not exist");

			var pokemonToDelete = _pokemonRepository.GetPokemon(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_pokemonRepository.DeletePokemon(pokemonToDelete))
			{
				ModelState.AddModelError("", "Something went wrong deleting pokemon");
			}

			return NoContent();
		}
	}
}
