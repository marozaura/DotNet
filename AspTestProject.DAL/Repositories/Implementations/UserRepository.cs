using AspTestProject.DAL.DataModels;
using AspTestProject.DAL.Entities;
using AspTestProject.DAL.Infrastructure;
using AspTestProject.DAL.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AspTestProject.DAL.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private IMapper _mapper;
    private AppDbContext _appDbContext;

    public UserRepository(AppDbContext context, IMapper mapper)
    {
        _appDbContext = context;
        _mapper = mapper;
    }

    public Task<UserLoginDataModel?> GetUserLoginDataAsync(string username)
    {
        return _appDbContext.Users.Select(u => new UserLoginDataModel
            {
                Id = u.Id,
                Email = u.Email,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                PasswordSalt = u.PasswordSalt
            })
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public void Create(UserCreateDataModel createDataModel)
    {
        var user = _mapper.Map<User>(createDataModel);
        //createDataModel.OrganizationIds
        //    .ForEach(organizationId => user.Organizations.Add(new Organization
        //    {
        //        Id  = organizationId
        //    }));
        _appDbContext.Users.Add(user);
    }
}