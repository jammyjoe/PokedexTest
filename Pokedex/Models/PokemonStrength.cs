using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokedexAPI.Models
{
    public partial class PokemonStrength
    {
        public int Id { get; set; }
        public int? PokemonId { get; set; }
        [Column("type_id")]
        public int? TypeId { get; set; }

        public virtual Pokemon? Pokemon { get; set; }
        public virtual PokemonType? Type { get; set; }
    }
}
