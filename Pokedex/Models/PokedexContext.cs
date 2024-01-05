using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PokedexAPI.Models
{
    public partial class PokedexContext : DbContext
    {
        public PokedexContext()
        {
        }

        public PokedexContext(DbContextOptions<PokedexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pokemon> Pokemons { get; set; } = null!;
        public virtual DbSet<PokemonResistance> PokemonResistances { get; set; } = null!;
        public virtual DbSet<PokemonType> PokemonTypes { get; set; } = null!;
        public virtual DbSet<PokemonWeakness> PokemonWeaknesses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.ToTable("pokemon");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Type2Id).HasColumnName("type2_id");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Pokemons)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("fk_type_id");
            });

            modelBuilder.Entity<PokemonResistance>(entity =>
            {
                entity.ToTable("pokemon_resistance");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.PokemonId).HasColumnName("pokemon_id");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Pokemon)
                    .WithMany(p => p.PokemonResistances)
                    .HasForeignKey(d => d.PokemonId)
                    .HasConstraintName("FK__pokemon_r__pokem__5441852A");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.PokemonResistances)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__pokemon_r__type___5535A963");
            });

            modelBuilder.Entity<PokemonType>(entity =>
            {
                entity.ToTable("pokemon_type");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type_name");
            });

            modelBuilder.Entity<PokemonWeakness>(entity =>
            {
                entity.ToTable("pokemon_weakness");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.PokemonId).HasColumnName("pokemon_id");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Pokemon)
                    .WithMany(p => p.PokemonWeaknesses)
                    .HasForeignKey(d => d.PokemonId)
                    .HasConstraintName("FK__pokemon_w__pokem__4D94879B");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.PokemonWeaknesses)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__pokemon_w__weakn__4E88ABD4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
