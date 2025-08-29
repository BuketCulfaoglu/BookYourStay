using BookYourStay.Domain.Entities;
using System.Linq.Expressions;

namespace BookYourStay.Application.Common.Interfaces
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        void Update(VillaNumber entity);
    }
}
