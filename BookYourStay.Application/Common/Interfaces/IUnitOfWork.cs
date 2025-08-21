namespace BookYourStay.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IVillaRepository Villa { get; set; }
        void Save();
    }
}
