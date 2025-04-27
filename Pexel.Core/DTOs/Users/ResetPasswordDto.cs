using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Core.DTOs.Users
{
    public class ResetPasswordDto
    {
        [EmailAddress]
        public string email { get; set; }
        public string token { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Compare("password")]
        public string confiermPassword { get; set; }
    }
}
