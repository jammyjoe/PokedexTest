using Microsoft.AspNetCore.Mvc.RazorPages;
using PokedexAPI.Models;
using PokedexAPI.RepositoryInterface;
using PokedexAPI.Repository;


namespace PokedexApp.Pages;

public class IndexModel : PageModel
{
    private readonly IPokemonRepository _repository;

    public IndexModel(IPokemonRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Pokemon> Pokemons { get; set; }
    public async Task OnGetAsync()
    {
        Pokemons = await _repository.GetPokemons();
    }
}