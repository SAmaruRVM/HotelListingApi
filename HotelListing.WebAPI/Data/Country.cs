using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelListing.WebAPI.Data
{
    public class Country
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string ShortName { get; set; }

        [JsonIgnore]
        public ICollection<Hotel> Hotels { get; } = new List<Hotel>();
    }
}
