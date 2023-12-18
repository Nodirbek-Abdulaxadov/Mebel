using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;
public class UnitOfWork(IImageInterface imageInterface,
                        IFurnitureInterface furnitureInterface,
                        IColorInterface colorInterface,
                        ICategoryInterface categoryInterface,
                        IOtpModelInterface otpModelInterface,
                        AppDbContext dbContext) : IUnitOfWork
{
    private readonly AppDbContext dbContext = dbContext;

    public IImageInterface Images { get; } = imageInterface;
    public IFurnitureInterface Furnitures { get; } = furnitureInterface;
    public IColorInterface Colors { get; } = colorInterface;
    public ICategoryInterface Categories { get; } = categoryInterface;

    public IOtpModelInterface OtpModels { get; } = otpModelInterface;

    public void Dispose()
        => GC.SuppressFinalize(this);

    public async Task SaveAsync()
        => await dbContext.SaveChangesAsync();
}