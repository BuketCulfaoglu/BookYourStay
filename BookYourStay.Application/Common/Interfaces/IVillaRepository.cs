using BookYourStay.Domain.Entities;
using System.Linq.Expressions;

namespace BookYourStay.Application.Common.Interfaces
{
    internal interface IVillaRepository
    {
        IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null);
        IEnumerable<Villa> Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null);
        void Add(Villa entity);
        void Update(Villa entity);
        void Remove(Villa entity);
        void Save();
    }
}
