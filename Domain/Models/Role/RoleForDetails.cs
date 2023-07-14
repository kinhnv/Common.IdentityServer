using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Role
{
    public class RoleForDetails
    {
        public RoleForDetails()
        {
            Name = String.Empty;
        }
        public Guid RoleId { get; set; }

        public string Name { get; set; }
    }
}
