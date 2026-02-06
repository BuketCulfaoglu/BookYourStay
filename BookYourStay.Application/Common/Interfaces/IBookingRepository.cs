using BookYourStay.Domain.Entities;
using System.Linq.Expressions;

namespace BookYourStay.Application.Common.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking entity);
    }
}
