using BookYourStay.Domain.Entities;

namespace BookYourStay.Application.Common.Interfaces
{
    public interface IVillaRepository : IRepository<Villa>
    {
        void Update(Villa entity);
        void Save();
    }
}
