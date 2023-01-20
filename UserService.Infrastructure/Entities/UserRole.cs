using i3rothers.Infrastructure.Repository;
using System.ComponentModel.DataAnnotations;

namespace UserService.Infrastructure.Entities
{
    public class UserRole : Entity
    {
        public UserRole()
        {
            IsDeleted = false;
        }
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }
}
