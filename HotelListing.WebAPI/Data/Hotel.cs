using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HotelListing.WebAPI.Data
{
    public class Hotel
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Address { get; set; }
        public double Rating { get; set; }

        public Country Country { get; set; }

        public int CountryId { get; set; }

    }
}
