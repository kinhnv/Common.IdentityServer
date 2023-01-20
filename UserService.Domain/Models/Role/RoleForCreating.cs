using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Models.Role
{
    public class RoleForCreating
    {
        public RoleForCreating()
        {
            Name = String.Empty;
        }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
    }
}
