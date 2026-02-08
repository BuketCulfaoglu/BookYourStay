using BookYourStay.Application.Common.Interfaces;
using BookYourStay.Domain.Entities;
using BookYourStay.Infrastructure.Data;

namespace BookYourStay.Infrastructure.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Booking entity)
        {
            _context.Bookings.Update(entity);
        }
    }
}