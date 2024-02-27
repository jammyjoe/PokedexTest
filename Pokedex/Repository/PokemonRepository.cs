using Microsoft.EntityFrameworkCore;
using Pokedex.DTOs;
using Pokedex.RepositoryInterface;
using PokedexAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokedexAPI.DTOs;

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

        //public async Task<string> GetPokemonType(int pokeId)
        //{
        //    var typeID = await _context.Pokemons
        //        .Where(p => p.Id == pokeId)
        //        .Select(p => p.Type1Id)
        //        .FirstOrDefaultAsync();

        //    return await _context.PokemonTypes
        //        .Where(t => t.Id == typeID)
        //        .Select(t => t.TypeName)
        //        .FirstOrDefaultAsync();
        //}

        public async Task<ICollection<Pokemon>> GetPokemons()
        {
            return await _context.Pokemons
                .Include(t => t.Type1)
                .Include(t => t.Type2)
                .Include(p => p.PokemonWeaknesses)
                    .ThenInclude(pw => pw.Type)
                .Include(p => p.PokemonStrengths)
                    .ThenInclude(pr => pr.Type)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }
        public async Task<Pokemon> GetPokemon(int id)
        {
            return await _context.Pokemons
                .Include(p => p.Type1)
                .Include(p => p.Type2)
                .Include(p => p.PokemonWeaknesses)
                    .ThenInclude(pw => pw.Type)
                .Include(p => p.PokemonStrengths)
                    .ThenInclude(ps => ps.Type)
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
                Name = pokemonDto.Name,
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

            if (pokemonDto.PokemonWeaknesses != null)
            {
                foreach (var weaknessDto in pokemonDto.PokemonWeaknesses)
                {
                    var weaknessTypeExists = await PokemonTypeExists(weaknessDto.Type.TypeName);
                    if (!weaknessTypeExists)
                    {
                        // Handle error if the provided  weakness type does not exist
                        // Similar to the handling for Type1 and Type2
                    }
                    else
                    {

                        var weaknessType =
                            await _context.PokemonTypes.FirstOrDefaultAsync(pt =>
                                pt.TypeName == weaknessDto.Type.TypeName);

                        var weakness = new PokemonWeakness()
                        {
                            Pokemon = pokemon,
                            Type = weaknessType
                        };
                        _context.PokemonWeaknesses.Add(weakness);
                    }
                }
            }

            if (pokemonDto.PokemonStrengths != null)
            {
                foreach (var strengthDto in pokemonDto.PokemonStrengths)
                {
                    var strengthTypeExists = await PokemonTypeExists(strengthDto.Type.TypeName);
                    if (!strengthTypeExists)
                    {
                        // Handle error if the provided strength type does not exist
                        // Similar to the handling for Type1 and Type2
                    }
                    else
                    {
                        var strengthType =
                            await _context.PokemonTypes.FirstOrDefaultAsync(pt =>
                                pt.TypeName == strengthDto.Type.TypeName);

                        var strength = new PokemonStrength
                        {
                            Pokemon = pokemon,
                            Type = strengthType
                        };
                        _context.PokemonStrengths.Add(strength);
                    }
                }
            }

            try
            {
                _context.Pokemons.Add(pokemon);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var exceptionMessage = ex.Message;
                var stackTrace = ex.StackTrace;
                var innerException = ex.InnerException;
            }
            return pokemon;
        }

        public async Task<bool> PokemonTypeExists(string typeName)
        {
            return await _context.PokemonTypes.AnyAsync(pt => pt.TypeName == typeName);
        }

        public async Task<bool> DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            return await SavePokemon();
        }

        //public async Task<bool> UpdatePokemon(Pokemon pokemon)
        //{
        //    _context.Update(pokemon);
        //    return await SavePokemon();
        //}

        public async Task<bool> UpdatePokemon(int id, PokemonDto updatedPokemonDto)
        {
            var existingPokemon = await _context.Pokemons
                .Include(p => p.Type1)
                .Include(p => p.Type2)
                .Include(p => p.PokemonStrengths)
                .ThenInclude(ps => ps.Type)
                .Include(p => p.PokemonWeaknesses)
                .ThenInclude(pw => pw.Type)
                .FirstOrDefaultAsync(p => p.Id == id);

            existingPokemon.Name = updatedPokemonDto.Name;
            await UpdateType(updatedPokemonDto, existingPokemon);

            try
            {
                _context.Update(existingPokemon);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                var exceptionMessage = ex.Message;
                var stackTrace = ex.StackTrace;
                var innerException = ex.InnerException;
                return false;
            }
        }

        public async Task<bool> UpdateType(PokemonDto updatePokemonDto, Pokemon existingPokemon)
        {
            //Update Types
            var Type1Exists = await PokemonTypeExists(updatePokemonDto.Type1.TypeName);
            var Type2Exists = await PokemonTypeExists(updatePokemonDto.Type2.TypeName);
            if (!Type1Exists)
            {
                return false;
            }

            existingPokemon.Type1.TypeName = updatePokemonDto.Type1.TypeName;

            if (updatePokemonDto.Type2.TypeName == "null")
            {
                updatePokemonDto.Type2 = null;
            }

            else if (!Type2Exists)
            {
                return false;
            }

            existingPokemon.Type2.TypeName = updatePokemonDto.Type2.TypeName;

            //Update weaknesses

            foreach (var weaknessDto in updatePokemonDto.PokemonWeaknesses)
            {
                //Checks if type exists
                var weaknessTypeExists = await PokemonTypeExists(weaknessDto.Type.TypeName);
                if (weaknessTypeExists)
                {
                    //Sets weaknesstype to be with FK in Type
                    var weaknessType = await _context.PokemonTypes
                        .FirstOrDefaultAsync(pt => pt.TypeName == weaknessDto.Type.TypeName);

                    weaknessDto.TypeId = weaknessType.Id;


                    var existingWeakness = existingPokemon.PokemonStrengths
                        .FirstOrDefault(ps => ps.TypeId == weaknessType.Id);

                    if (existingWeakness != null)
                    {
                        existingWeakness.TypeId = weaknessType.Id;
                        //existingWeakness.Type.Id = weaknessType.Id;
                    }
                    else
                    {
                        // Add new weakness
                        var newWeakness = new PokemonWeakness()
                        {
                            PokemonId = existingPokemon.Id,
                            TypeId = weaknessType.Id
                        };
                        existingPokemon.PokemonWeaknesses.Add(newWeakness);
                    }
                }
            }

            //Update strength

            foreach (var strengthDto in updatePokemonDto.PokemonStrengths)
            {
                var strengthTypeExists = await PokemonTypeExists(strengthDto.Type.TypeName);
                if (strengthTypeExists)
                {
                    var strengthType = await _context.PokemonTypes
                        .FirstOrDefaultAsync(pt => pt.TypeName == strengthDto.Type.TypeName);

                    strengthDto.TypeId = strengthType.Id;

                    var existingStrength = existingPokemon.PokemonStrengths
                        .FirstOrDefault(ps => ps.TypeId == strengthType.Id);

                    if (existingStrength != null)
                    {
                        existingStrength.TypeId = strengthType.Id;
                        //existingStrength.Type.Id = strengthType.Id;
                    }
                    else
                    {
                        // Add new strength
                        var newStrength = new PokemonStrength()
                        {
                            PokemonId = existingPokemon.Id,
                            TypeId = strengthType.Id
                        };
                        existingPokemon.PokemonStrengths.Add(newStrength);
                    }
                }

            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                // Handle any exceptions during database update
                var exceptionMessage = ex.Message;
                var stackTrace = ex.StackTrace;
                var innerException = ex.InnerException;
                return false;
            }
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