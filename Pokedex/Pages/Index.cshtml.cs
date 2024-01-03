using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokedex.Models;
using Pokedex.RepositoryInterface;

namespace PokedexAPI.Pages
{
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
}
