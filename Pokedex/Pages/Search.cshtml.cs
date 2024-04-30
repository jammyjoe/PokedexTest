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
    public IEnumerable<Pokemon> FilteredPokemon { get; set; }

    public List<PokemonType> PokemonTypes { get; set; }

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

    public async Task OnPostAsync()
    {
        //if (Search.Name == null)
        //{
        //    ModelState.Clear();
        //    Message = "Not found";
        //    return;
        //}

        //if (!await _pokemonRepository.PokemonExists(Search.Name))
        //{
        //    Message = "This Pokemon does not exist";
        //    return;
        //}

        //Pokemon = await _pokemonRepository.GetPokemon(Search.Name);
        //ModelState.Clear();
        //Message = "Successfully retrieved Pokemon";

        if (!string.IsNullOrEmpty(Search.Name))
        {
            // Fetch the specific Pokemon based on the search name
            Pokemon = await _pokemonRepository.GetPokemon(Search.Name);
            if (Pokemon != null)
            {
                Message = "";
            }
            else
            {
                Message = "Pokemon not found";
            }
        }
    }

    public async Task OnGetAsync()
    {
        FilteredPokemon = await _pokemonRepository.GetPokemons();


        //if (SelectedTypes != null || SelectedTypes.Any())
        //{
        //    FilteredPokemon = await _typeRepository.GetPokemonsByType(SelectedTypes);
        //}
        //else
        //{
        //    Message = "This Pokemon does not exist";
        //}
    }
}
