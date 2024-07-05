using System;
using AspTestProject.DAL.DataModels;
using AspTestProject.DAL.Entities;

namespace AspTestProject.DAL.Repositories.Interfaces
{
    public interface IOrganiztionRepository
    {
        Task<List<OrganizationDataModel>> GetAllAsync();
    }
}
