using AspTestProject.BLL.Models;

namespace AspTestProject.BLL.Services.Interfaces;

public interface IOrganizationService
{
    Task<List<OrganiztionModel>> GetAllAsync();
}