using AspTestProject.BLL.Enums;
using AspTestProject.BLL.Models;
using AspTestProject.BLL.Services.Interfaces;
using AspTestProject.DAL.Repositories.Interfaces;

namespace AspTestProject.BLL.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    private readonly IPasswordValidationService _passwordValidationService;
    private readonly ITokenGeneratorService _tokenGeneratorService;

    public AuthService(IUserRepository userRepository, IPasswordValidationService passwordValidationService, ITokenGeneratorService tokenGeneratorService)
    {
        _userRepository = userRepository;
        _passwordValidationService = passwordValidationService;
        _tokenGeneratorService = tokenGeneratorService;
    }

    public async Task<LoginResponseModel> PerformLoginAsync(LoginRequestModel loginRequestModel)
    {
        var loginResponseModel = new LoginResponseModel { Status = LoginResultEnum.NotFound };
        var userLoginData = await _userRepository.GetUserLoginDataAsync(loginRequestModel.UserName);

        if (userLoginData != null)
        {
            var isPasswordValid = _passwordValidationService
                .ValidatePassword(loginRequestModel.Password, userLoginData.PasswordHash, userLoginData.PasswordSalt);

            if (isPasswordValid)
            {
                loginResponseModel = new LoginResponseModel
                {
                    Status = LoginResultEnum.Ok,
                    Username = userLoginData.Username,
                    Token = _tokenGeneratorService.GenerateJwt(userLoginData.Id, userLoginData.Email)
                };
            }
        }

        return loginResponseModel;
    }
}