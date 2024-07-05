using AspTestProject.BLL.Models;
using AspTestProject.DAL.DataModels;
using AutoMapper;

namespace AspTestProject.BLL.Mapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<OrganizationDataModel, OrganiztionModel>();
        }
    }
}
