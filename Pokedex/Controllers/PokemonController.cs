using System.Data.Entity;
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
		public ActionResult<Pokemon> GetPokemons()
		{
			var pokemons = _pokemonRepository.GetPokemons();

			if (!ModelState.IsValid)
				return NoContent();

			return Ok(pokemons);
		}

		[HttpGet("{id}")]
		public ActionResult<Pokemon> GetPokemon(int id)
		{
			var pokemon = _pokemonRepository.GetPokemon(id);

			if (!ModelState.IsValid)
				return NoContent();

			return Ok(pokemon);
		}

		[HttpPost("{id}")]
		public ActionResult<Pokemon> CreatePokemon(int id,
			[FromBody] Pokemon pokemonCreate)
		{
			if (pokemonCreate == null)
				return BadRequest("This Id is invalid");

			_context.Add(pokemonCreate);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			_context.SaveChanges();

			return Ok(pokemonCreate);
		}


		[HttpPut("{id}")]
		public ActionResult<Pokemon> UpdatePokemon(int id,
	[FromBody] Pokemon pokemonCreate)
		{
			if (pokemonCreate == null)
				return BadRequest("This Id is invalid");

			_context.Update(pokemonCreate);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			_context.SaveChanges();

			return Ok(pokemonCreate);
		}


		[HttpDelete("{id}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public ActionResult<Pokemon> DeletePokemon(int id)
		{
			var pokemonToDelete = _pokemonRepository.GetPokemon(id);

			if (pokemonToDelete == null)
				return NotFound("Sorry, but this pokemon doesn't exist");

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
