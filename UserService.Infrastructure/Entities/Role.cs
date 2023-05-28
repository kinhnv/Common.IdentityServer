using i3rothers.Infrastructure.Entities.Attributes;
using i3rothers.Infrastructure.Repository;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace UserService.Infrastructure.Entities
{
    [DatabaseTable(Name = "Roles")]
    public class Role : Entity
    {
        [DatabaseGuidColumn(IsKey = true)]
        public Guid RoleId { get; set; }

        [DatabaseStringColumn(IsRequired = true, StringLength = 64)]
        public string Name { get; set; } = null!;
    }
}
