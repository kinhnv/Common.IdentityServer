using i3rothers.Infrastructure.Entities.Attributes;
using i3rothers.Infrastructure.Repository;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace UserService.Infrastructure.Entities
{
    [DatabaseTable(Name = "UserRoles")]
    public class UserRole : Entity
    {
        [DatabaseGuidColumn(IsKey = true)]
        public Guid UserId { get; set; }

        [DatabaseGuidColumn(IsKey = true)]
        public Guid RoleId { get; set; }
    }
}
