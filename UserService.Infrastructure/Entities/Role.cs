using i3rothers.Infrastructure.Repository;
using System.ComponentModel.DataAnnotations;

namespace UserService.Infrastructure.Entities
{
    public class Role : Entity
    {
        public Role()
        {
            Name = string.Empty;
            IsDeleted = false;
        }

        [Required]
        public Guid RoleId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }
    }
}
