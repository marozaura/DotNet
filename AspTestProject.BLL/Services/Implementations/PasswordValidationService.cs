using AspTestProject.BLL.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace AspTestProject.BLL.Services.Implementations;

public class PasswordValidationService : IPasswordValidationService
{
    public bool ValidatePassword(string inputPassword, byte[] userPassword, string salt)
    {
        var inputPasswordHash = GeneratePasswordHash(inputPassword, salt);
        return inputPasswordHash.SequenceEqual(userPassword);
    }

    public byte[] GeneratePasswordHash(string inputPassword, string salt)
    {
        var plainTextBytes = Encoding.Unicode.GetBytes(inputPassword);
        var saltBytes = Encoding.Unicode.GetBytes(salt);

        var combinedBytes = new byte[plainTextBytes.Length + salt.Length];
        Buffer.BlockCopy(plainTextBytes, 0, combinedBytes, 0, plainTextBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, combinedBytes, plainTextBytes.Length, salt.Length);

        HashAlgorithm hashAlgorithm = new SHA256Managed();
        var hash = hashAlgorithm.ComputeHash(combinedBytes);
        return hash;
    }
}