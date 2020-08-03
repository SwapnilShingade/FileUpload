using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IOStream = System.IO.Stream;

namespace ImageChecker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var path = "C:/Users/Admin/Downloads/Resume/qrcode.png";            
            var imageFileStream = System.IO.File.OpenRead(path);
            //var fileToUpload =  File(imageFileStream, "image/png");
            HttpContent fileStreamContent = new StreamContent(imageFileStream);
           
            var uploadUrl = " http://goqr.me/api/doc/read-qr-code/?file=";
            using (var client = new HttpClient())
            {                
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                var formData = new MultipartFormDataContent() {};
                formData.Add(fileStreamContent);               
                HttpResponseMessage response =  client.PostAsync(uploadUrl, formData).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Ok(response.Content.ReadAsStringAsync());
                    Console.WriteLine(response.Content.ReadAsStringAsync());
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
            return null;
        }
    }
}
