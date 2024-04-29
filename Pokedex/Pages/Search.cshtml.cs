using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokedex.RepositoryInterface;
using PokedexAPI.DTOs;
using PokedexAPI.Models;
using PokedexAPI.RepositoryInterface;
using PokedexAPI.ViewModels;


public class SearchModel : PageModel
{
    private readonly IPokemonRepository _pokemonrepository;
    private readonly ITypeRepository _typeRepository;

    public SearchModel(IPokemonRepository pokemonrepository, ITypeRepository typeRepository)
    {
        _pokemonrepository = pokemonrepository;
        _typeRepository = typeRepository;
    }

    public Pokemon Pokemon { get; set; }
    public IEnumerable<PokemonType> PokemonTypes { get; set; }

    public string Message { get; set; }

    [BindProperty]
    public SearchViewModel Search { get; set; } = new SearchViewModel();

    public async Task OnGetAsync()
    {
        // Retrieve the list of Pokemon types from the repository
        PokemonTypes = await _typeRepository.GetTypes();
    }

    public async Task OnPostAsync()
    {
        if (Search.Name == null)
        {
            ModelState.Clear();
            Message = "Not found";
            return;
        }

        if (!await _pokemonrepository.PokemonExists(Search.Name))
        {
            Message = "This Pokemon does not exist";
            return;
        }

        Pokemon = await _pokemonrepository.GetPokemon(Search.Name);
        ModelState.Clear();
        Message = "Successfully retrieved Pokemon";
    }
}
