using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PokedexAPI.DTOs;
using PokedexAPI.Models;
using PokedexAPI.RepositoryInterface;

namespace Pokedex.Controllers;

[ApiController]
[Route("api/[controller]")]
[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
public class TypeController : ControllerBase
{
    private readonly PokedexContext _context;
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;

    public TypeController(PokedexContext context, ITypeRepository typeRepository, IMapper mapper)
    {
        _context = context;
        _typeRepository = typeRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<Type>> GetTypes()
    {
        var types = _mapper.Map<List<PokemonTypeDto>>(await _typeRepository.GetTypes());

        if (!ModelState.IsValid)
            return NoContent();

        return Ok(types);
    }
}
