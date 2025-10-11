using BookYourStay.Application.Common.Interfaces;
using BookYourStay.Infrastructure.Data;

namespace BookYourStay.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IVillaRepository Villa { get; set; }
        public IAmenityRepository Amenity { get; set; }
        public IVillaNumberRepository VillaNumber { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Villa = new VillaRepository(_context);
            Amenity = new AmenityRepository(_context);
            VillaNumber = new VillaNumberRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}