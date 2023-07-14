using i3rothers.Infrastructure.Entities.Attributes;
using i3rothers.Infrastructure.Repository;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Infrastructure.Entities
{
    [DatabaseTable(Name = "Users")]
    public class User : Entity
    {
        [DatabaseGuidColumn(IsKey = true)]
        public Guid UserId { get; set; }

        [DatabaseStringColumn(IsRequired = true, StringLength = 64)]
        public string UserName { get; set; } = null!;

        [DatabaseStringColumn(IsRequired = true, StringLength = 64)]
        public string Password { get; set; } = null!;

        [DatabaseStringColumn(IsRequired = true, StringLength = 64)]
        public string FirstName { get; set; } = null!;

        [DatabaseStringColumn(IsRequired = true, StringLength = 64)]
        public string LastName { get; set; } = null!;
    }
}
