using System.ComponentModel.DataAnnotations;

namespace PokedexApp.ViewModels;

public class SearchViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
