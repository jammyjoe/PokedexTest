using System.ComponentModel.DataAnnotations;

namespace PokedexAPI.ViewModels;

public class SearchViewModel
{
    [Required]
    public int Id { get; set; }
}
