using AspTestProject.DAL.DataModels;

namespace AspTestProject.DAL.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserLoginDataModel?> GetUserLoginDataAsync(string username);
    void Create(UserCreateDataModel createDataModel);
}