using AspTestProject.BLL.Models;
using AspTestProject.BLL.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspTestProject.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateModel createViewModel)
        {
            var createModel = _mapper.Map<UserCreateModel>(createViewModel);
            await _userService.CreateAsync(createModel);
            return Ok();
        }
    }
}
