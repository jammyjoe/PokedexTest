using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokedex.RepositoryInterface;
using PokedexAPI.Models;

public class AddModel : PageModel
{
    private readonly IPokemonRepository _repository;

    public AddModel(IPokemonRepository repository)
    {
        _repository = repository;
    }
    [BindProperty] public Pokemon Pokemon { get; set; }

    public void OnGet()
    {
        // Optional: You can perform any necessary setup logic when the page is initially requested
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Check if the model is valid based on data annotations and other validations
        if (!ModelState.IsValid)
        {
            return Page(); // Return to the form with validation errors
        }

        // TODO: Add logic to save the Pokemon to the database or perform any other actions

        // Optional: Redirect to a different page after successfully adding the Pokemon
        return RedirectToPage("/Index");
    }
}

