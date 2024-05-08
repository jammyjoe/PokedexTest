using System.ComponentModel.DataAnnotations;

namespace PokedexAPI.ViewModels;

public class SearchViewModel
{
    [Required]
    public string Name { get; set; }
}
