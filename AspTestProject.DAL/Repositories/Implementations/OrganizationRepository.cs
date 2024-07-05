using AspTestProject.DAL.DataModels;
using AspTestProject.DAL.Infrastructure;
using AspTestProject.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspTestProject.DAL.Repositories.Implementations;

public class OrganizationRepository: IOrganiztionRepository
{
    private AppDbContext _appDbContext;
    public OrganizationRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<OrganizationDataModel>> GetAllAsync()
    {
       return await _appDbContext.Organizations
            .Select(x => new OrganizationDataModel
            {
                Id = x.Id,
                Name = x.Name,
                UserName = x.User.Name
            }).ToListAsync();
    }
}