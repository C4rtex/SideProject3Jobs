using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TopTenJobPages.Models;
using TopTenJobPages.Services;

namespace TopTenJobPages.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var service = new ObtainJobsService();
        var listjobs = service.obtenerLinkedinJobs();
        return View(listjobs);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

