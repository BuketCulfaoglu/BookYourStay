namespace BookYourStay.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IVillaRepository Villa { get; }
        IVillaNumberRepository VillaNumber { get; }
        IApplicationUserRepository User { get; }
        IAmenityRepository Amenity { get; }
        IBookingRepository Booking { get; }
        void Save();
    }
}
