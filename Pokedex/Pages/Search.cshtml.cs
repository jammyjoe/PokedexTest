using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokedex.Models;
using Pokedex.RepositoryInterface;
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
            if (Search.Id != null && Search.Id > 0)
            {
                Pokemon = await _repository.GetPokemon(Search.Id);
                ModelState.Clear();
                Message = "Successfully retrieved Pokemon";
            }
            else
            {
                ModelState.Clear();
                Message = "Not found";
            }
        }
    }
}
