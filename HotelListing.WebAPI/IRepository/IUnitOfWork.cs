using HotelListing.WebAPI.Data;

using System;
using System.Threading.Tasks;

namespace HotelListing.WebAPI.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<int, Country> Countries { get; }
        IGenericRepository<int, Hotel> Hotels { get; }

        Task SaveChangesAsync();
        void SaveChanges();
    }
}
