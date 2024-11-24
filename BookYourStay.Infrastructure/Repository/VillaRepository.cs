using BookYourStay.Application.Common.Interfaces;
using BookYourStay.Domain.Entities;
using BookYourStay.Infrastructure.Data;

namespace BookYourStay.Infrastructure.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Villa entity)
        {
            _context.Update(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}