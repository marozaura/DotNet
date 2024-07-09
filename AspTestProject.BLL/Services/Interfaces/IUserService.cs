using AspTestProject.BLL.Models;

namespace AspTestProject.BLL.Services.Interfaces;

public interface IUserService
{
    Task CreateAsync(UserCreateModel model);
}