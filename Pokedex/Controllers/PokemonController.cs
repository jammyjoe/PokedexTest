using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokedex.DTOs;
using Pokedex.RepositoryInterface;
using PokedexAPI.Models;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
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

        //Controller Methods


        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Pokemon>> GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(await _pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
                return NoContent();

            return Ok(pokemons);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<Pokemon>> GetPokemon(int id)
        {
            if (!(await _pokemonRepository.PokemonExists(id)))
                return NotFound("This pokemon does not exist");

            var pokemon = _mapper.Map<PokemonDto>(await _pokemonRepository.GetPokemon(id));

            if (!ModelState.IsValid)
                return NoContent();

            return Ok(pokemon);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<Pokemon>> GetPokemon(string name)
        {
            if (!(await _pokemonRepository.PokemonExists(name)))
                return NotFound("This pokemon does not exist");

            var pokemon = _mapper.Map<PokemonDto>(await _pokemonRepository.GetPokemon(name));

            if (!ModelState.IsValid)
                return NoContent();

            return Ok(pokemon);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PokemonDto>> CreatePokemon(PokemonDto pokemonCreate)
        {

            if (pokemonCreate == null)
            {
                return BadRequest("This Id is invalid");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdPokemon = await _pokemonRepository.CreatePokemon(pokemonCreate);

                // Map the created Pokemon entity back to DTO for response
                var createdPokemonDto = _mapper.Map<PokemonDto>(createdPokemon);

                return CreatedAtAction(nameof(GetPokemon), new { id = createdPokemonDto.Id }, createdPokemonDto);
            }
            catch (Exception ex)
            {
                // Handle exceptions or errors during Pokemon creation
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        //public async Task<ActionResult<Pokemon>> CreatePokemon(int id,
        //	[FromBody] Pokemon pokemonCreate)
        //{
        //	if (pokemonCreate == null)
        //		return BadRequest("This Id is invalid");

        //	if (!ModelState.IsValid)
        //		return BadRequest(ModelState);

        //	var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

        //	if (!(await _pokemonRepository.CreatePokemon(pokemonMap)))
        //	{
        //		ModelState.AddModelError("", "Something went wrong while savin");
        //		return StatusCode(500, ModelState);
        //	}
        //	return Ok("Succesfully Created");
        //}


        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Pokemon>> UpdatePokemon(int id,
    [FromBody] Pokemon pokemonUpdate)
        {
            if (pokemonUpdate == null)
                return BadRequest("This Id is invalid");

            if (!(await _pokemonRepository.PokemonExists(id)))
                return NotFound("This pokemon does not exist");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(pokemonUpdate);

            if (!(await _pokemonRepository.UpdatePokemon(pokemonMap)))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Pokemon>> DeletePokemon(int id)
        {
            if (!(await _pokemonRepository.PokemonExists(id)))
                return NotFound("This pokemon does not exist");

            var pokemonToDelete = (await _pokemonRepository.GetPokemon(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!(await _pokemonRepository.DeletePokemon(pokemonToDelete)))
            {
                ModelState.AddModelError("", "Something went wrong deleting pokemon");
            }
            return NoContent();
        }

        //Converst TypeId to TypeName
        //[HttpGet("type/{typeId:int}")]
        //[ProducesResponseType(200)]
        //public async Task<ActionResult<PokemonType>> GetType(int typeId)
        //{
        //    var type = _mapper.Map<string>(await _pokemonRepository.GetTypeById(typeId));

        //    if (!ModelState.IsValid)
        //        return NoContent();

        //    return Ok(type);
        //}

        //Converts PokeId to TypeName
        //[HttpGet("pokemontype/{typeId:int}")]
        //[ProducesResponseType(200)]
        //public async Task<ActionResult<PokemonType>> GetPokemonType(int typeId)
        //{
        //    var type = _mapper.Map<string>(await _pokemonRepository.GetPokemonType(typeId));

        //    if (!ModelState.IsValid)
        //        return NoContent();

        //    return Ok(type);
        //}

    }
}
