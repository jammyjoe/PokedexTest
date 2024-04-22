using PokedexAPI.Models;
using PokedexAPI.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pokedex.DTOs;
using PokedexAPI.DTOs;

namespace PokedexAPI.Repository;

public class TypeRepository : ITypeRepository
{
    private readonly PokedexContext _context;

    public TypeRepository(PokedexContext context)
    {
        _context = context;
    }

    public async Task<List<PokemonType>> GetTypes()
    {
        return await _context.PokemonTypes.ToListAsync();
    }

    public async Task<List<Pokemon>> GetPokemonsByType(string typeName)
    {
        var pokemonType = await _context.PokemonTypes.FirstOrDefaultAsync(t => t.TypeName == typeName);

        if (pokemonType == null)
        {
            //return error this type doesnt exist
        }

        var pokemons = await _context.Pokemons
               .Include(p => p.Type1)
               .Include(p => p.Type2)
               .Include(p => p.PokemonStrengths)
                    .ThenInclude(ps => ps.Type)
               .Include(p => p.PokemonWeaknesses)
                    .ThenInclude(pw => pw.Type)
               .Where(p => p.Type1.TypeName == typeName || p.Type2.TypeName == typeName)
               .ToListAsync();

        return pokemons;
    }
}
