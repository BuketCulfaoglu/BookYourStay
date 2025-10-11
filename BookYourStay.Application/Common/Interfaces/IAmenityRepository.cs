using BookYourStay.Domain.Entities;
using System.Linq.Expressions;

namespace BookYourStay.Application.Common.Interfaces
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
        void Update(Amenity entity);
    }
}
