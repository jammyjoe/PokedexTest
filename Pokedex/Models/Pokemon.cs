using System;
using System.Collections.Generic;

namespace PokedexAPI.Models
{
    public partial class Pokemon
    {
        public Pokemon()
        {
            PokemonResistances = new HashSet<PokemonResistance>();
            PokemonWeaknesses = new HashSet<PokemonWeakness>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? TypeId { get; set; }
        public int? Type2Id { get; set; }
        public virtual PokemonType? Type { get; set; }
        public virtual ICollection<PokemonResistance> PokemonResistances { get; set; }
        public virtual ICollection<PokemonWeakness> PokemonWeaknesses { get; set; }
    }
}
