using HotelListing.WebAPI.Data;
using HotelListing.WebAPI.IRepository;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger)
            => (_unitOfWork, _logger) = (unitOfWork, logger);


        [HttpGet("[action]")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(IList<Country>))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> All()
        {
            try
            {
                _logger.LogInformation($"A new request was made to the {nameof(All)} action inside of the {nameof(Country)} controller");
                return Ok(await _unitOfWork.Countries.GetAllAsync(includeExpressions: nameof(Country.Hotels)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("[action]/{id:int}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Country))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> ById(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries
                                                .GetSingleAsync<ICollection<Hotel>>(expression: country => country.Id == id, includeExpressions: country => country.Hotels);



                return country is null ? NotFound("Country not found 404 http status code") : Ok(country);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
