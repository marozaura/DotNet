using AspTestProject.BLL.Models;
using AspTestProject.BLL.Services.Interfaces;
using AspTestProject.DAL.DataModels;
using AspTestProject.DAL.Repositories.Interfaces;
using AutoMapper;

namespace AspTestProject.BLL.Services.Implementations;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordValidationService _passwordValidationService;

    public UserService(IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository, IPasswordValidationService passwordValidationService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordValidationService = passwordValidationService;
    }


    public async Task CreateAsync(UserCreateModel model)
    {
        var passwordSalt = Guid.NewGuid().ToString();
        var passwordHash = _passwordValidationService.GeneratePasswordHash(model.Password, passwordSalt);

        var createDataModel = _mapper.Map<UserCreateDataModel>(model);
        createDataModel.PasswordHash = passwordHash;
        createDataModel.PasswordSalt = passwordSalt;

        _userRepository.Create(createDataModel);
        await _unitOfWork.CommitAsync();
    }
}