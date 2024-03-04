using Microsoft.AspNetCore.Mvc;

namespace PokedexAPI.ViewModels;
public class AddViewModel : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
