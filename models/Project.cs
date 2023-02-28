using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace tickeing_system.models
{
    public class Project
    {
       [Key]
       public int Id { get; set; }
       [StringLength(500)]
       public string Description { get; set; }
       public int CreatorId { get; set; }
       public List<Issue>? Issues { get; set; }
    }
}