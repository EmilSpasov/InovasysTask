using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Inovasys.Data.Models
{
    public class Contragent : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required] 
        public string DdsNumber { get; set; }
    }
}
