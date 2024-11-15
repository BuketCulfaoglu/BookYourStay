using BookYourStay.Application.Common.Interfaces;
using BookYourStay.Domain.Entities;
using System.Linq.Expressions;
using BookYourStay.Infastructure.Data;

namespace BookYourStay.Infastructure.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Villa> Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public void Add(Villa entity)
        {
            _context.Add(entity);
        }

        public void Update(Villa entity)
        {
            _context.Update(entity);
            //_context.Villas.Update(entity);
        }

        public void Remove(Villa entity)
        {
            _context.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
