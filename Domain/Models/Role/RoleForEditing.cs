using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Role
{
    public class RoleForEditing
    {
        public RoleForEditing()
        {
            Name = String.Empty;
        }
        [Required]
        public Guid RoleId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }
    }
}
