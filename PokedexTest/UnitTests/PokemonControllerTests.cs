using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Controllers;
using Pokedex.DTOs;
using Pokedex.RepositoryInterface;
using PokedexAPI.Models;

namespace Pokedex.Test.UnitTests;

public class PokemonControllerTests
{
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IMapper _mapper;

    public PokemonControllerTests()
    {
        _pokemonRepository = A.Fake<IPokemonRepository>();
        _mapper = A.Fake<IMapper>();
    }

    //[SetUp]
    //public void Setup()
    //{
    //}

    [Test]
    public async Task PokemonController_GetPokemon_ReturnOk()
    {
        //Arrange
        var pokemons = A.Fake<ICollection<PokemonDto>>();
        var pokemonList = A.Fake<List<PokemonDto>>();
        A.CallTo(() => _mapper.Map<List<PokemonDto>>(pokemons)).Returns(pokemonList);
        var controller = new PokemonController(_pokemonRepository, _mapper);

        //Act
        var results = await controller.GetPokemons();

        //Assert
        results.Should().NotBeNull();

        //Not 100% sure this line actually tests
        results.Should().BeOfType(typeof(ActionResult<Pokemon>)); 
        //Assert.That(results, Is.Not.Null);
        //Assert.That(results, Is.TypeOf(typeof(OkObjectResult)));

    }
}