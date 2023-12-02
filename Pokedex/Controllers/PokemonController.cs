using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;

namespace Pokedex.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PokemonController : ControllerBase
	{
		private readonly PokedexContext _context;

		public PokemonController(PokedexContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult<Pokemon> GetPokemons()
		{
			var pokemons = _context.Pokemons.OrderBy(p => p.Id).ToList();

			if (!ModelState.IsValid)
				return NoContent();

			return Ok(pokemons);
		}

		[HttpGet("{id}")]
		public ActionResult<Pokemon> GetPokemon(int id)
		{
			var pokemon = _context.Pokemons.FirstOrDefault(p => p.Id == id);

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
		public ActionResult<Pokemon> DeletePokemon(int id)
		{
			var pokemonToDelete = _context.Pokemons.Any(p => p.Id == id);
			if (pokemonToDelete == null)
				return NotFound("Sorry, but this pokemon doesn't exist");

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			_context.Remove(true);

			_context.SaveChanges();

			return Ok(_context);
		}
	}
}
