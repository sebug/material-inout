using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZXing.PDF417;
using ZXing.ImageSharp.Rendering;
using ZXing.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Png;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaterialInOut.Controllers
{
    [Route("api/[controller]")]
    public class BarCodeValuesController : Controller
    {
        // GET api/values/5
        [HttpGet("{id}")]
        public FileContentResult Get(string id)
        {
            var writer = new BarcodeWriter<Rgba32>
            {
                Format = ZXing.BarcodeFormat.QR_CODE
            };
            var image = writer.Write(id);
            var pngEncoder = new PngEncoder();
            using (var ms = new MemoryStream())
            {
                image.Save(ms, pngEncoder);
                return File(ms.ToArray(), "image/png");
            }
        }
    }
}

