using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PokedexAPI.DTOs;

namespace PokedexAPI.ViewModels;
public class AddViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Type1 { get; set; }
    public string Type2 { get; set; }

}
