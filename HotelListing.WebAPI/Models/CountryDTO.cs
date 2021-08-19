using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.WebAPI.Models
{

    public class CreateCountryDTO
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(2)]
        [Required]
        public string ShortName { get; set; }
    }

    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public ICollection<HotelDTO> Hotels { get; } = new List<HotelDTO>();
    }
}
