using Microsoft.AspNetCore.Mvc;
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

		public PokemonController(PokedexContext context, IPokemonRepository pokemonRepository)
		{
			_context = context;
			_pokemonRepository = pokemonRepository;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public ActionResult<Pokemon> GetPokemons()
		{
			var pokemons = _pokemonRepository.GetPokemons();

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

			var pokemon = _pokemonRepository.GetPokemon(id);

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

			if(!_pokemonRepository.CreatePokemon(pokemonCreate));
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
	[FromBody] Pokemon pokemonCreate)
		{
			if (pokemonCreate == null)
				return BadRequest("This Id is invalid");

			if (!_pokemonRepository.PokemonExists(id))
				return NotFound("This pokemon does not exist");

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if(!_pokemonRepository.UpdatePokemon(pokemonCreate));
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
