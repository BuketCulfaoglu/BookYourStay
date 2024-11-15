using BookYourStay.Application.Common.Interfaces;
using BookYourStay.Domain.Entities;
using System.Linq.Expressions;

namespace BookYourStay.Infastructure.Repository
{
    public class VillaRepository : IVillaRepository
    {
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
            throw new NotImplementedException();
        }

        public void Update(Villa entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Villa entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
