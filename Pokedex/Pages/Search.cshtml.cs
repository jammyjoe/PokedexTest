using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokedex.RepositoryInterface;
using PokedexAPI.Models;
using PokedexAPI.ViewModels;

namespace PokedexAPI.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IPokemonRepository _repository;

        public SearchModel(IPokemonRepository repository)
        {
            _repository = repository;
        }

        public Pokemon Pokemon { get; set; }

        public string Message { get; set; }

        [BindProperty] 
        public SearchViewModel Search { get; set; } = new SearchViewModel();

        public async Task OnPostAsync()
        {
            if (Search.Name == null)
            {
                ModelState.Clear();
                Message = "Not found";
                return;
            }

            if (!await _repository.PokemonExists(Search.Name))
            {
                Message = "This Pokemon does not exist";
                return;
            }

            Pokemon = await _repository.GetPokemon(Search.Name);
            ModelState.Clear();
            Message = "Successfully retrieved Pokemon";
        }
    }
}
