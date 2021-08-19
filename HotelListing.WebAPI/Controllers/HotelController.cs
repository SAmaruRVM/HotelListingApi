using AutoMapper;

using HotelListing.WebAPI.Data;
using HotelListing.WebAPI.IRepository;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;
        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, Mapper mapper)
            => (_unitOfWork, _logger, _mapper) = (unitOfWork, logger, mapper);


        [HttpGet("[action]")]
        [ProducesResponseType(statusCode: 200, Type = typeof(IList<Hotel>))]
        [ProducesResponseType(statusCode: 500, Type = typeof(string))]
        public async Task<IActionResult> All()
        {
            try
            {
                _logger.LogInformation($"A new request was made to the {nameof(All)} action inside of the {nameof(Hotel)} controller");
                return Ok(await _unitOfWork.Hotels
                                           .GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Hotel))]
        [ProducesResponseType(statusCode: 500, Type = typeof(string))]
        [ProducesResponseType(statusCode: 404, Type = typeof(string))]
        public async Task<IActionResult> ById(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels
                                             .GetSingleAsync<Country>(expression: hotel => hotel.Id == id,
                                                                     includeExpressions: hotel => hotel.Country);

                if (hotel is null)
                    return NotFound("Hotel with the specified Id was not found.");


                return Ok(hotel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal Server error. Please try again later.");
            }
        }
    }
}
