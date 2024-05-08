using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokedex.DTOs;
using Pokedex.RepositoryInterface;
using PokedexAPI.DTOs;
using PokedexAPI.Models;
using PokedexAPI.RepositoryInterface;
using PokedexAPI.ViewModels;


public class SearchModel : PageModel
{
    private readonly IPokemonRepository _pokemonRepository;
    private readonly ITypeRepository _typeRepository;

    public SearchModel(IPokemonRepository pokemonRepository, ITypeRepository typeRepository)
    {
        _pokemonRepository = pokemonRepository;
        _typeRepository = typeRepository;
    }

    public Pokemon Pokemon { get; set; }
    public IEnumerable<Pokemon> Pokemons { get; set; }
    public List<PokemonType> PokemonTypes { get; set; }

    [BindProperty]
    public List<string> SelectedTypes { get; set; }
    public string Message { get; set; }

    [BindProperty]
    public SearchViewModel Search { get; set; } = new SearchViewModel();

    //public async Task OnGetAsync()
    //{
    //    PokemonTypes = await _typeRepository.GetTypes();

    //    Search.Id = 0;
    //    Search.Name = "";
    //}

    public async Task OnGetAsync()
    {
        PokemonTypes = await _typeRepository.GetTypes();
        Pokemons = await _pokemonRepository.GetPokemons();
    }

    public async Task<IActionResult> OnGetApplyFilterAsync()
    {
        PokemonTypes = await _typeRepository.GetTypes();

        if (SelectedTypes == null || SelectedTypes.Count == 0)
        {
            // No types selected, return all Pokemons
            Pokemons = await _pokemonRepository.GetPokemons();
        }
        else
        {
            // Fetch Pokemons based on selected types
            Pokemons = await _typeRepository.GetPokemonsByType(SelectedTypes);
        }
        return Page();
    }
    public async Task OnPostAsync()
    {

        // Check if a search query parameter is provided
        if (!string.IsNullOrEmpty(Search?.Name))
        {
            // Perform the search operation
            Pokemon = await _pokemonRepository.GetPokemon(Search.Name);
            if (Pokemon != null)
            {
                // If the Pokemon is found, populate the message
                Message = "Successfully retrieved Pokemon";
            }
            else
            {
                // If the Pokemon is not found, set an appropriate message
                Message = "Pokemon not found";
            }
        }
        else
        {
            // If no search query parameter is provided, retrieve all Pokemons
            Pokemons = await _pokemonRepository.GetPokemons();
        }
    }
}
