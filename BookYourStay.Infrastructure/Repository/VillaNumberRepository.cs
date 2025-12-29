using System.Linq.Expressions;
using BookYourStay.Application.Common.Interfaces;
using BookYourStay.Domain.Entities;
using BookYourStay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookYourStay.Infrastructure.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaNumberRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<VillaNumber> GetAll(Expression<Func<VillaNumber, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<VillaNumber> query = _context.Set<VillaNumber>();

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties
                             .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query.ToList();
        }

        public VillaNumber? Get(Expression<Func<VillaNumber, bool>> filter, string? includeProperties = null)
        {
            IQueryable<VillaNumber> query = _context.Set<VillaNumber>();

            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties
                             .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query.FirstOrDefault();
        }

        public void Add(VillaNumber entity)
        {
            _context.Add(entity);
        }

        public void Update(VillaNumber entity)
        {
            _context.VillaNumbers.Update(entity);
        }

        public void Remove(VillaNumber entity)
        {
            _context.Remove(entity);
        }

    }
}
