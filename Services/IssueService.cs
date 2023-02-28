using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tickeing_system.models;
using Microsoft.EntityFrameworkCore;

namespace tickeing_system.Services
{
    public class IssueService : IIssueService
    {
        IProjectService _projectService;
        private TicketingSystemContex _context;

        public IssueService(IProjectService service, TicketingSystemContex context) {
         _projectService = service;
        _context = context;
        }

        // Service to get issue by issueId
        public Issue GetIssueDetailsById(int issueId)
        {
            List<Issue> issueList;
            Issue issue;
            try{
                issueList = _context.Issues.Include(s=>s.labels).ToList();
                issue = issueList.Find(e=>e.IssueId==issueId);
            }
            catch (Exception) {
                throw;
            }
            return issue;
        }
        
        // Service to get issue by issueTitle
        public Issue GetIssueByTitle(string issueTitle)
        {
            List<Issue> issueList;
            Issue issue;
            try{
                issueList = GetIssueList();
                issue = issueList.Find(e=>e.IssueTitle ==issueTitle);
            }
            catch (Exception) {
                throw;
            }
            return issue;
        }

        // Services to get all issues
        public List<Issue> GetIssueList()
        {
            List<Issue> issueList;
            try {
                issueList = _context.Issues.Include(s=>s.labels).ToList();
                
            } catch (Exception) {
                throw;
            }
            return issueList;
        }

        // Service to add issue labels
        public ResponseModel AddIssueLabel(models.LabelView labelModel){
            ResponseModel model = new ResponseModel();
            try
            {
                Issue _tempIssue = GetIssueDetailsById(labelModel.IssueId);
                if(_tempIssue != null){
                    _tempIssue.labels.Add(new models.Label {LabelName=labelModel.LabelName});
                    _context.Update<Issue> (_tempIssue);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Labels Added Successfully";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        // Service to update project issue
        public ResponseModel UpdateProjectIssue(int issueId, IssueView issueModel){
            ResponseModel model = new ResponseModel();
            try
            {
                Issue _tempIssue = GetIssueDetailsById(issueId);
                if(_tempIssue != null){
                    _tempIssue.IssueType = issueModel.IssueType;
                    _tempIssue.IssueTitle = issueModel.IssueTitle;
                    _tempIssue.IssueDescription = issueModel.IssueDescription;
                    _tempIssue.IssueStatus = issueModel.IssueStatus;
                    _tempIssue.IssueReporterId = issueModel.IssueReporterId;
                    _tempIssue.IssueAssigneeId = issueModel.IssueAssigneeId;
                    _tempIssue.labels.Add(new models.Label {LabelName=issueModel.IssueLabel});
                    _context.Update<Issue> (_tempIssue);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Project Issue Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        // Service to change to issue status
        public ResponseModel UpdateIssueToNextStatus(int issueId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Issue _tempIssue = GetIssueDetailsById(issueId);
                String[] issueStatusList = {"Open","InProgress","InReview","CodeComplete","QA Testing","Done Status"};
                if(_tempIssue != null){
                    if (Array.IndexOf(issueStatusList,_tempIssue.IssueStatus)>=5){
                        model.Message = "Issue Already resolved";
                    }
                    else{
                        String temp = issueStatusList[Array.IndexOf(issueStatusList,_tempIssue.IssueStatus)+1];
                        _tempIssue.IssueStatus = temp;
                        _context.Update<Issue> (_tempIssue);
                         _context.SaveChanges();
                        model.IsSuccess = true;
                        model.Message = "Issue Status Updated Successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        // Service to rest issue status to open
        public ResponseModel ResetIssueStatus(int issueId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Issue _tempIssue = GetIssueDetailsById(issueId);
                if(_tempIssue != null){
                    _tempIssue.IssueStatus = "Open";
                    _context.Update<Issue> (_tempIssue);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Issue Status Set To Open";
                }

            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        // Service to create issue
        public ResponseModel SaveIssue(IssueView issueModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Project _tempProject = _projectService.GetProjectDetailsById(issueModel.projectId);
                if (_tempProject != null){
                    if (_tempProject.Issues.Any()){
                        _tempProject.Issues.Add(
                            new Issue{
                            IssueId = issueModel.IssueId,
                            IssueType = issueModel.IssueType,
                            IssueTitle = issueModel.IssueTitle,
                            IssueDescription = issueModel.IssueDescription,
                            IssueStatus = issueModel.IssueStatus,
                            IssueReporterId = issueModel.IssueReporterId,
                            IssueAssigneeId = issueModel.IssueAssigneeId,
                            labels = new List<models.Label>{
                                    new models.Label {LabelName=issueModel.IssueLabel}
                                    }
                            }
                         );
                    }
                    else
                    {
                        List<Issue>_tempIssue = new List<Issue>{
                        new Issue{
                            IssueId = issueModel.IssueId,
                            IssueType = issueModel.IssueType,
                            IssueTitle = issueModel.IssueTitle,
                            IssueDescription = issueModel.IssueDescription,
                            IssueStatus = issueModel.IssueStatus,
                            IssueReporterId = issueModel.IssueReporterId,
                            IssueAssigneeId = issueModel.IssueAssigneeId,
                            labels = new List<models.Label>{
                                    new models.Label {LabelName=issueModel.IssueLabel}
                                    }
                            }
                        };
                        _tempProject.Issues = _tempIssue;
                    }
                     _context.Update< Project > (_tempProject);
                     _context.SaveChanges();
                     model.IsSuccess = true;
                     model.Message = "Issue Added Successfully";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        // Service to delete issue label
        public ResponseModel DeleteIssueLabel(int issueId, int labelId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Issue _tempIssue = GetIssueDetailsById(issueId);

                if(_tempIssue != null){
                    _tempIssue.labels.RemoveAll(x=>x.LabelId==labelId);
                    _context.Update<Issue> (_tempIssue);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Label Deleted Successfully";
                }
            }
            catch (Exception ex) {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }
    }
}