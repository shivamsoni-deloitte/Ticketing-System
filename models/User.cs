using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace tickeing_system.models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        [StringLength(20)]
        public string UserPassword { get; set; }

        [StringLength(15)]
        public string UserRole { get; set; }

    }
}