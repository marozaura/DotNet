using AspTestProject.DAL.DataModels;
using AspTestProject.DAL.Entities;
using AutoMapper;

namespace AspTestProject.DAL.Mapper;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserCreateDataModel, User>();
    }
}