using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tickeing_system.models
{
    public class Label
    {
        [Key]
        public int LabelId { get; set; }

        [StringLength(20)]
        public string? LabelName { get; set; }
    }
}