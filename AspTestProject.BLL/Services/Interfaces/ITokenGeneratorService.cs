namespace AspTestProject.BLL.Services.Interfaces;

public interface ITokenGeneratorService
{
    string GenerateJwt(long userId, string userEmail);
}