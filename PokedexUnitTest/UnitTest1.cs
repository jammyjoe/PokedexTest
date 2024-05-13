using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Controllers;
using Pokedex.DTOs;
using Pokedex.RepositoryInterface;
using PokedexAPI.Models;

[TestFixture]
public class PokemonControllerTests
{
    private PokedexContext _fakeContext;
    private IPokemonRepository _fakePokemonRepository;
    private IMapper _fakeMapper;
    private PokemonController _pokemonController;

    [SetUp]
    public void Setup()
    {
        _pokemonController = new PokemonController(_fakeContext, _fakePokemonRepository, _fakeMapper);
        _fakePokemonRepository = A.Fake<IPokemonRepository>();
        _fakeMapper = A.Fake<IMapper>();
    }

    [Test]
    public async Task PokemonController_GetPokemons_ReturnsOkObjectResult()
    {
        // Arrange
        var pokemons = A.Fake<List<Pokemon>>();
        A.CallTo(() => _fakePokemonRepository.GetPokemons()).Returns(pokemons);
        var controller = new PokemonController(_fakeContext, _fakePokemonRepository, _fakeMapper);

        // Act
        var result = await controller.GetPokemons();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
    }
}
