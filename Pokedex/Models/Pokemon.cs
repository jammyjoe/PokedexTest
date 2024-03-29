﻿using System;
using System.Collections.Generic;

namespace PokedexAPI.Models
{
    public partial class Pokemon
    {
        public Pokemon()
        {
            PokemonStrengths = new HashSet<PokemonStrength>();
            PokemonWeaknesses = new HashSet<PokemonWeakness>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Type1Id { get; set; }
        public int? Type2Id { get; set; }

        public virtual PokemonType Type1 { get; set; }
        public virtual PokemonType? Type2 { get; set; }

        public virtual ICollection<PokemonStrength> PokemonStrengths { get; set; }
        public virtual ICollection<PokemonWeakness> PokemonWeaknesses { get; set; }
    }
}
