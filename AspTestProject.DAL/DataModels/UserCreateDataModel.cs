namespace AspTestProject.DAL.DataModels
{
    public class UserCreateDataModel
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public List<int> OrganizationIds { get; set; }
    }
}
