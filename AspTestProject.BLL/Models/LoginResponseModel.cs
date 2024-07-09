using AspTestProject.BLL.Enums;

namespace AspTestProject.BLL.Models
{
    public class LoginResponseModel
    {
        public string Username { get; set; }
        public LoginResultEnum Status { get; set; }
        public string Token { get; set; }
    }
}
