using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokedex.DTOs;
using Pokedex.RepositoryInterface;
using PokedexAPI.DTOs;
using PokedexAPI.Models;
using PokedexAPI.ViewModels;

public class AddModel : PageModel
{
    private readonly IPokemonRepository _repository;

    public AddModel(IPokemonRepository repository)
    {
        _repository = repository;
    }
    [BindProperty] public string Name { get; set; }
    [BindProperty] public string Type1 { get; set; }
    [BindProperty] public string Type2 { get; set; }
     public PokemonDto Pokemon { get; set; }
    [BindProperty] public AddViewModel Add { get; set; } = new AddViewModel();



    public void OnGet()
    {
        // Optional: You can perform any necessary setup logic when the page is initially requested
    }

    public async Task<IActionResult> OnPostAsync(AddViewModel add)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Call the repository method to create the Pokemon
        await _repository.CreatePokemon(Pokemon);

        // Redirect to a different page after successfully adding the Pokemon
        return RedirectToPage("/Home");
    }
}

