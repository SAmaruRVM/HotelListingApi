using System.ComponentModel.DataAnnotations;

namespace HotelListing.WebAPI.Models
{
    public class CreateHotelDTO
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Range(1, 5)]
        public double Rating { get; set; }
    }

    public class HotelDTO : CreateHotelDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }
}
