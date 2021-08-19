using AutoMapper;

using HotelListing.WebAPI.Data;
using HotelListing.WebAPI.Models;

namespace HotelListing.WebAPI.Configurations
{
    public sealed class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
        }
    }
}
