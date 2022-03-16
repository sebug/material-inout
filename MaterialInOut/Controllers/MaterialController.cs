using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialInOut.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaterialInOut.Controllers
{
    public class MaterialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}