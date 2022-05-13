using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialInOut.Models;
using MaterialInOut.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MaterialInOut.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialController(IMaterialRepository materialRepository)
        {
            this._materialRepository = materialRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Bill(BillRequestViewModel? request)
        {
            var response = new BillResponseViewModel();
            response.MaterialItems = new List<MaterialItem>();
            if (request != null)
            {
                response.Name = request.Name;
                if (!string.IsNullOrEmpty(request.EANs))
                {
                    response.MaterialItems = request.EANs.Split(',')
                        .Select(ean => this._materialRepository.GetItem(ean))
                        .ToList();
                }
            }
            return View(response);
        }
    }
}