using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public string Name { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        [JsonIgnore]
        public virtual PokemonType Type { get; set; }

        public int? Type2Id { get; set; }
        [ForeignKey("Type2Id")]
        [JsonIgnore]
        public virtual PokemonType? Type2 { get; set; }

        public virtual ICollection<PokemonResistance> PokemonResistances { get; set; }
        public virtual ICollection<PokemonWeakness> PokemonWeaknesses { get; set; }
    }
}
