using AspTestProject.BLL.Enums;
using AspTestProject.BLL.Models;
using AspTestProject.BLL.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspTestProject.Controllers
{
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            var loginResponseModel = await _authService.PerformLoginAsync(loginRequestModel);

            return loginResponseModel.Status switch
            {
                LoginResultEnum.Ok => new OkObjectResult(loginResponseModel),
                LoginResultEnum.NotFound => new NotFoundResult(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [Authorize]
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Response.Cookies.Delete("UserId");
            HttpContext.Response.Cookies.Delete("UserEmail");

            return new SignOutResult(new[]
            {
                CookieAuthenticationDefaults.AuthenticationScheme
            });
        }
    }
}
