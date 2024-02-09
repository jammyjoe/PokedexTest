using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexAPI.Models
{
    public partial class PokemonStrength
    {
        public int Id { get; set; }
        public int? PokemonId { get; set; }
        [Column("type_id")]
        public int? TypeId { get; set; }

        [JsonIgnore]
        public virtual Pokemon? Pokemon { get; set; }
        [JsonIgnore]
        public virtual PokemonType? Type { get; set; }
    }
}
