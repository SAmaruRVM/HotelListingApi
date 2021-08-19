using HotelListing.WebAPI.Data;
using HotelListing.WebAPI.IRepository;

using Microsoft.AspNetCore.Mvc;

using Serilog;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountriesController(IUnitOfWork unitOfWork) => (_unitOfWork) = (unitOfWork);


        [HttpGet("[action]")]
        [ProducesResponseType(statusCode: 200, Type = typeof(IList<Country>))]
        public async Task<OkObjectResult> GetAll()
        {
            Log.Information("There was a new http request to see all of the countries.");


            return Ok((await _unitOfWork
                  .Countries
                  .GetAllAsync(expression: null, includeExpressions: nameof(Country.Hotels))
                  ).Select(country => new { Name = country.Name, Hotels = country.Hotels }));
        }


        [HttpGet("[action]")]
        [ProducesResponseType(statusCode: 200, Type = typeof(IList<Hotel>))]
        public async Task<OkObjectResult> GetHoteis() => Ok(await _unitOfWork.Hotels.GetAllAsync(expression: null,
                                                         includeExpressions: nameof(Hotel.Country)));

    }
}
