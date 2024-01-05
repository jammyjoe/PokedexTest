using System;
using System.Collections.Generic;

namespace PokedexAPI.Models
{
    public partial class PokemonType
    {
        public PokemonType()
        {
            PokemonResistances = new HashSet<PokemonResistance>();
            PokemonWeaknesses = new HashSet<PokemonWeakness>();
            Pokemons = new HashSet<Pokemon>();
        }

        public int Id { get; set; }
        public string? TypeName { get; set; }

        public virtual ICollection<PokemonResistance> PokemonResistances { get; set; }
        public virtual ICollection<PokemonWeakness> PokemonWeaknesses { get; set; }
        public virtual ICollection<Pokemon> Pokemons { get; set; }
    }
}
