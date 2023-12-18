using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;
public class OtpModelRepository(AppDbContext dbContext)
    : Repository<OtpModel>(dbContext), IOtpModelInterface
{
}