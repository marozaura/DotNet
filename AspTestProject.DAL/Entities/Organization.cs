namespace AspTestProject.DAL.Entities
{
    public class Organization
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
