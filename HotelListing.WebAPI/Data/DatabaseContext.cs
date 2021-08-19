using Microsoft.EntityFrameworkCore;

namespace HotelListing.WebAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> contextOptions) : base(contextOptions) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>()
                        .HasData(new[]
                        {
                            new Country
                            {
                               Id = 1,
                               Name = "Portugal",
                               ShortName = "PT"
                            },
                            new Country
                            {
                               Id = 2,
                               Name = "Itália",
                               ShortName = "IT"
                            },
                            new Country
                            {
                               Id = 3,
                               Name = "Jamaica",
                               ShortName = "JM"
                            }
                        });


            modelBuilder.Entity<Hotel>()
                        .HasData(new[]
                        {
                            new Hotel
                            {
                                Id = 1,
                                Name = "Sandals Resort And Spa",
                                Address = "Negril",
                                CountryId = 3,
                                Rating = 4.5d
                            },
                            new Hotel
                            {
                                Id = 2,
                                Name = "Hotel Tróia",
                                Address = "Rua dos Marinheiros",
                                CountryId = 1,
                                Rating = 3.2d
                            },
                            new Hotel
                            {
                                Id = 3,
                                Name = "Grand Palldium",
                                Address = "Nassua",
                                CountryId = 2,
                                Rating = 4.9d
                            }
                        });
        }
    }
}
