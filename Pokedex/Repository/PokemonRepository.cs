using Microsoft.EntityFrameworkCore;
using Pokedex.DTOs;
using Pokedex.RepositoryInterface;
using PokedexAPI.Models;
using System;

namespace Pokedex.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokedexContext _context;

        public PokemonRepository(PokedexContext context)
        {
            _context = context;
        }

        //Getting Type Id -> TypeName (Could be useful method later)
        //public async Task<string> GetTypeById(int typeId)
        //{
        //    return await _context.PokemonTypes
        //        .Where(t => t.Id == typeId)
        //        .Select(t => t.TypeName)
        //        .FirstOrDefaultAsync();
        //}

        public async Task<string> GetPokemonType(int pokeId)
        {
            var typeID = await _context.Pokemons
                .Where(p => p.Id == pokeId)
                .Select(p => p.Type1Id)
                .FirstOrDefaultAsync();

            return await _context.PokemonTypes
                .Where(t => t.Id == typeID)
                .Select(t => t.TypeName)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Pokemon>> GetPokemons()
        {
            return await _context.Pokemons
                .Include(t => t.Type1)
                .Include(t => t.Type2)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }
        public async Task<Pokemon> GetPokemon(int id)
        {
            return await _context.Pokemons
                .Include(p => p.Type1)
                .Include(p => p.Type2)
                .Include(p => p.PokemonWeaknesses)
                .Include(p => p.PokemonResistances)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pokemon> GetPokemon(string name)
        {
            return await _context.Pokemons
                .Include(p => p.Type1)
                .Include(p => p.Type2)
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Pokemon> CreatePokemon(PokemonDto pokemonDto)
        {
            var pokemon = new Pokemon
            {
                Id = pokemonDto.Id,
                Name = pokemonDto.Name
            };

            var type1Exists = await PokemonTypeExists(pokemonDto.Type1.TypeName);
            if (!type1Exists)
            {
                // throw new Exception("Invalid Type1 specified");
            }
            else
            {
                var type1 = await _context.PokemonTypes.FirstOrDefaultAsync(pt => pt.TypeName == pokemonDto.Type1.TypeName);
                pokemon.Type1Id = type1.Id;
                pokemon.Type1 = type1;
            }

            if (pokemonDto.Type2 != null)
            {
                var type2Exists = await PokemonTypeExists(pokemonDto.Type2.TypeName);
                if (!type2Exists)
                {
                    // Handle error if the provided Type2 does not exist
                    // Similar to the handling for Type1
                }
                else
                {
                    var type2 = await _context.PokemonTypes.FirstOrDefaultAsync(pt => pt.TypeName == pokemonDto.Type2.TypeName);
                    pokemon.Type2Id = type2.Id;
                    pokemon.Type2 = type2;

                }
            }

            try
            {
                _context.Pokemons.Add(pokemon);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Access the inner exception for details
                var innerException = ex.InnerException;
                // Log or handle the exception as needed
            }

            return pokemon;
        }

        public async Task<bool> PokemonTypeExists(string typeName)
        {
            return await _context.PokemonTypes.AnyAsync(pt => pt.TypeName == typeName);
        }

        //      public async Task<bool> CreatePokemon(Pokemon pokemon)
        //{
        //	_context.Add(pokemon);
        //	return await SavePokemon();
        //}

        public async Task<bool> DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            return await SavePokemon();
        }

        public async Task<bool> UpdatePokemon(Pokemon pokemon)
        {
            _context.Update(pokemon);
            return await SavePokemon();
        }

        public async Task<bool> PokemonExists(int id)
        {
            return await _context.Pokemons.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> PokemonExists(string name)
        {
            return await _context.Pokemons.AnyAsync(p => p.Name == name);
        }
        public async Task<bool> SavePokemon()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}