using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tickeing_system.models
{
    public class IssueView
    {
        [Key]
        public int IssueId { get; set; }

        [StringLength(20)]
        public string IssueType { get; set; }

        [StringLength(20)]
        public string IssueTitle { get; set; }

        [StringLength(200)]
        public string IssueDescription { get; set; }

        public string IssueStatus{ get; set; }

        public int IssueReporterId { get; set; }

        public int IssueAssigneeId { get; set; }

        [StringLength(20)]
        public string? IssueLabel { get; set; }

        public int projectId { get; set; }
    }
}