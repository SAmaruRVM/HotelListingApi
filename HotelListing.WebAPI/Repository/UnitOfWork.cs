using HotelListing.WebAPI.Data;
using HotelListing.WebAPI.IRepository;

using System;
using System.Threading.Tasks;

namespace HotelListing.WebAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public UnitOfWork(DatabaseContext context)
            => (_context, Countries, Hotels) = (context, new GenericRepository<int, Country>(context), new GenericRepository<int, Hotel>(context));


        public IGenericRepository<int, Country> Countries { get; }

        public IGenericRepository<int, Hotel> Hotels { get; }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void SaveChanges() => _context.SaveChanges();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
