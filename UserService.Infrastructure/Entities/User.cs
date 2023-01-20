using i3rothers.Infrastructure.Repository;
using System.ComponentModel.DataAnnotations;

namespace UserService.Infrastructure.Entities
{
    public class User : Entity
    {
        [Required]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(64)]
        public string UserName { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;
    }
}
