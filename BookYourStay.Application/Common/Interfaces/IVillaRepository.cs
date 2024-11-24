using BookYourStay.Domain.Entities;
using System.Linq.Expressions;

namespace BookYourStay.Application.Common.Interfaces
{
    public interface IVillaRepository : IRepository<Villa>
    {
        void Update(Villa entity);
        void Save();
    }
}
