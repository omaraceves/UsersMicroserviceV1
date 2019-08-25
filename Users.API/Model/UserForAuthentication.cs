using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Users.API.Model
{
    public class UserForAuthentication
    {
        [Required]
        [MaxLength(50)]
        public string UserEmail { get; set; }

        [Required]
        [MaxLength(16)]
        public string Password { get; set; }
    }
}
