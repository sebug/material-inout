using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialInOut.Models;
using MaterialInOut.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaterialInOut.Controllers
{
    [Route("api/[controller]")]
    public class MaterialItemsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMaterialRepository _materialRepository;

        public MaterialItemsController(ILogger<HomeController> logger,
            IMaterialRepository materialRepository)
        {
            _logger = logger;
            _materialRepository = materialRepository;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<MaterialItem> Get()
        {
            return this._materialRepository.GetItems();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

