using AspTestProject.BLL.Models;
using AspTestProject.BLL.Services.Interfaces;
using AspTestProject.DAL.Repositories.Interfaces;
using AutoMapper;

namespace AspTestProject.BLL.Services.Implementations
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganiztionRepository _organizationRepository;
        private readonly IMapper _mapper;

        public OrganizationService(IOrganiztionRepository organizationRepository, 
            IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<List<OrganiztionModel>> GetAllAsync()
        {
            var organizationDataModels = await _organizationRepository.GetAllAsync();
            return _mapper.Map<List<OrganiztionModel>>(organizationDataModels);
        }
    }
}
