using System;
using System.Collections.Generic;

namespace PokedexAPI.Models
{
    public partial class PokemonType
    {
        public PokemonType()
        {
            PokemonStrengths = new HashSet<PokemonStrength>();
            PokemonType1s = new HashSet<Pokemon>();
            PokemonType2s = new HashSet<Pokemon>();
            PokemonWeaknesses = new HashSet<PokemonWeakness>();
        }

        public int Id { get; set; }
        public string? TypeName { get; set; }

        public virtual ICollection<PokemonStrength> PokemonStrengths { get; set; }
        public virtual ICollection<Pokemon> PokemonType1s { get; set; }
        public virtual ICollection<Pokemon> PokemonType2s { get; set; }
        public virtual ICollection<PokemonWeakness> PokemonWeaknesses { get; set; }
    }
}
