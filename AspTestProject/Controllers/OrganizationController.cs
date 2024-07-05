using AspTestProject.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizationModels = await _organizationService.GetAllAsync();
            return Ok(organizationModels);
        }
    }
}
