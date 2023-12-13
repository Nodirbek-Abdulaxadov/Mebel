namespace DataAccessLayer.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IImageInterface Images { get; }
    IFurnitureInterface Furnitures { get; }
    IColorInterface Colors { get; }
    ICategoryInterface Categories { get; }
    Task SaveAsync();
}