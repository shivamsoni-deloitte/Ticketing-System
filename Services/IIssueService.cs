using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tickeing_system.models;

namespace tickeing_system.Services
{
    public interface IIssueService
    {
        ResponseModel SaveIssue(IssueView issueModel);

        Issue GetIssueDetailsById(int issueId);

        List<Issue> GetIssueList();
        ResponseModel AddIssueLabel(LabelView labelModel);

        ResponseModel UpdateProjectIssue(int issueId, IssueView issueModel);

        ResponseModel UpdateIssueToNextStatus(int issueId);

        ResponseModel ResetIssueStatus(int issueId);

        ResponseModel DeleteIssueLabel(int issueId, int labelId);

        Issue GetIssueByTitle(string issueTitle);
    }
}