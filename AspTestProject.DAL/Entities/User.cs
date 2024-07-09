using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspTestProject.DAL.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime CreatedDateTimeUtc { get; set; }
        public DateTime UpdatedDateTimeUtc { get; set; }
        public ICollection<Organization> Organizations { get; set; } = new List<Organization>();
    }
}
