using PokedexAPI.Models;
using PokedexAPI.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

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
}
