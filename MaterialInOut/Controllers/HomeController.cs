using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MaterialInOut.Models;
using MaterialInOut.Repositories;

namespace MaterialInOut.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMaterialRepository _materialRepository;

    public HomeController(ILogger<HomeController> logger,
        IMaterialRepository materialRepository)
    {
        _logger = logger;
        _materialRepository = materialRepository;
    }

    public IActionResult Index(MaterialContentModel? materialContent = null)
    {
        if (this.Request.Method == "POST" && materialContent != null)
        {
            byte[] excelBytes;
            using (var ms = new MemoryStream())
            {
                materialContent.MaterialFile.CopyTo(ms);
                excelBytes = ms.ToArray();
            }
            _materialRepository.ImportExcelFile(excelBytes);
        }
        return View();
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

