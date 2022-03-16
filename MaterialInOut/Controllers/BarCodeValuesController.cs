﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaterialInOut.Controllers
{
    [Route("api/[controller]")]
    public class BarCodeValuesController : Controller
    {
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return id;
        }
    }
}

