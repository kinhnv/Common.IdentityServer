using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Models.User
{
    public class UserForEditing
    {
        public UserForEditing()
        {
            UserName = String.Empty;
            Password = string.Empty;
            RoleIds = new List<Guid>();
        }
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(64)]
        public string UserName { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        public List<Guid> RoleIds { get; set; }
    }
}
