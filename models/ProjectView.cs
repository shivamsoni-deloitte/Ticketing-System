using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tickeing_system.models
{
    public class ProjectView
    {
       [Key]
       public int Id { get; set; }

       [StringLength(500)]
       public string Description { get; set; }

       public int UserId { get; set; }
    }
}