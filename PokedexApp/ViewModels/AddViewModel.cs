using Microsoft.AspNetCore.Mvc;

namespace PokedexApp.ViewModels;
public class AddViewModel : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
