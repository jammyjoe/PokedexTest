﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokedexAPI.Models;

#nullable disable

namespace PokedexAPI.Migrations
{
    [DbContext(typeof(PokedexContext))]
    partial class PokedexContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Latin1_General_CI_AS")
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PokedexAPI.Models.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("name");

                    b.Property<int>("Type1Id")
                        .HasColumnType("int")
                        .HasColumnName("type1_id");

                    b.Property<int?>("Type2Id")
                        .HasColumnType("int")
                        .HasColumnName("type2_id");

                    b.HasKey("Id");

                    b.HasIndex("Type1Id");

                    b.HasIndex("Type2Id");

                    b.ToTable("pokemon", (string)null);
                });

            modelBuilder.Entity("PokedexAPI.Models.PokemonResistance", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("PokemonId")
                        .HasColumnType("int")
                        .HasColumnName("pokemon_id");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("type_id");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId");

                    b.HasIndex("TypeId");

                    b.ToTable("pokemon_resistance", (string)null);
                });

            modelBuilder.Entity("PokedexAPI.Models.PokemonType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("TypeName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("type_name");

                    b.HasKey("Id");

                    b.ToTable("pokemon_type", (string)null);
                });

            modelBuilder.Entity("PokedexAPI.Models.PokemonWeakness", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("PokemonId")
                        .HasColumnType("int")
                        .HasColumnName("pokemon_id");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("type_id");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId");

                    b.HasIndex("TypeId");

                    b.ToTable("pokemon_weakness", (string)null);
                });

            modelBuilder.Entity("PokedexAPI.Models.Pokemon", b =>
                {
                    b.HasOne("PokedexAPI.Models.PokemonType", "Type1")
                        .WithMany("Pokemons")
                        .HasForeignKey("Type1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_type_id");

                    b.HasOne("PokedexAPI.Models.PokemonType", "Type2")
                        .WithMany()
                        .HasForeignKey("Type2Id");

                    b.Navigation("Type1");

                    b.Navigation("Type2");
                });

            modelBuilder.Entity("PokedexAPI.Models.PokemonResistance", b =>
                {
                    b.HasOne("PokedexAPI.Models.Pokemon", "Pokemon")
                        .WithMany("PokemonResistances")
                        .HasForeignKey("PokemonId")
                        .HasConstraintName("FK__pokemon_r__pokem__5441852A");

                    b.HasOne("PokedexAPI.Models.PokemonType", "Type")
                        .WithMany("PokemonResistances")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK__pokemon_r__type___5535A963");

                    b.Navigation("Pokemon");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("PokedexAPI.Models.PokemonWeakness", b =>
                {
                    b.HasOne("PokedexAPI.Models.Pokemon", "Pokemon")
                        .WithMany("PokemonWeaknesses")
                        .HasForeignKey("PokemonId")
                        .HasConstraintName("FK__pokemon_w__pokem__4D94879B");

                    b.HasOne("PokedexAPI.Models.PokemonType", "Type")
                        .WithMany("PokemonWeaknesses")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK__pokemon_w__weakn__4E88ABD4");

                    b.Navigation("Pokemon");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("PokedexAPI.Models.Pokemon", b =>
                {
                    b.Navigation("PokemonResistances");

                    b.Navigation("PokemonWeaknesses");
                });

            modelBuilder.Entity("PokedexAPI.Models.PokemonType", b =>
                {
                    b.Navigation("PokemonResistances");

                    b.Navigation("PokemonWeaknesses");

                    b.Navigation("Pokemons");
                });
#pragma warning restore 612, 618
        }
    }
}
