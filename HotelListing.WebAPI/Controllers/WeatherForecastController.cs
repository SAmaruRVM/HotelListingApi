using Microsoft.AspNetCore.Mvc;

using Serilog;

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace HotelListing.WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(IEnumerable<WeatherForecast>))]
        public OkObjectResult Get()
        {
            var rng = new Random();

            Log.Information("A request to get the Weather Forecast was made.");
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }

        [HttpGet("[action]/{numero:int}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(ExpandoObject))]
        public OkObjectResult Quadrado(int numero) => Ok(new { Numero = numero * numero });
    }
}
