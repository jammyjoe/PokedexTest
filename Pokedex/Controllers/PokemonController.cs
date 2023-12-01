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
		public ActionResult<Pokemon> GetPokemon()
		{
			var pokedex = _context.Pokemons.OrderBy(p => p.Id).ToList();

			if (!ModelState.IsValid)
				return NoContent();

			return Ok(pokedex);
		}


    }
}
