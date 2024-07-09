using AspTestProject.BLL.Models;

namespace AspTestProject.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseModel> PerformLoginAsync(LoginRequestModel loginRequestModel);
}