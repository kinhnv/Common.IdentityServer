using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class UserForList
    {
        public UserForList()
        {
            UserName = String.Empty;
        }
        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}
