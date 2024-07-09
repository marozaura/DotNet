namespace AspTestProject.DAL.DataModels;

public class UserLoginDataModel
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
}