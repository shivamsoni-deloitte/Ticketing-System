using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tickeing_system.models;

namespace tickeing_system.Services
{
    public class ProjectService : IProjectService
    {
        IUserService _userService;
        private TicketingSystemContex _context;
        public ProjectService(IUserService service, TicketingSystemContex context){
            _userService = service;
            _context = context;
        }
        
        // Service to delete project
        public ResponseModel DeleteProject(int projectId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Project _tempProject = GetProjectDetailsById(projectId);
                if (_tempProject != null){
                    _context.Remove < Project > (_tempProject);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Project Deleted Successfully";
                }else{
                    model.IsSuccess = false;
                    model.Message = "Project Not Found";
                }
            }
            catch (Exception ex) {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        // Service to delete project issue
        public ResponseModel DeleteProjectIssue(int projectId, int issueId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Project _tempProject = GetProjectDetailsById(projectId);
                if(_tempProject!=null){
                    _tempProject.Issues.RemoveAll(x=>x.IssueId==issueId);
                    _context.Update<Project> (_tempProject);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Issue Deleted Successfully";
                }
            }
            catch (Exception ex) {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        // Service to get project by project by Id
        public Project GetProjectDetailsById(int projectId)
        {
            List < Project > projectList;
            Project project;
            try {
                projectList = GetProjectList();
                project = projectList.Find(e=>e.Id == projectId);
            } catch (Exception) {
                throw;
            }
            return project;
        }

        // Service to get all projects
        public List<Project> GetProjectList()
        {
            List < Project > projectList;
            try {
                projectList = _context.Projects.Include(s=>s.Issues).ThenInclude(s=>s.labels).ToList();
                
            } catch (Exception) {
                throw;
            }
            return projectList;
        }
        
        // Service to save project
        public ResponseModel SaveProject(ProjectView projectModel)
        {
            
            ResponseModel model = new ResponseModel();
            try
            {
                // User userDetail = _userService.GetUserById(projectModel.UserId);
                // Console.WriteLine(userDetail.UserPassword);
                Project project = new Project(){
                    Description = projectModel.Description,
                    CreatorId = projectModel.UserId
                };
                _context.Add<Project>(project);
                model.Message = "Project Created Successfully";
                _context.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        // Service to update project
        public ResponseModel UpdateProject(ProjectView projectModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Project _tempProject = GetProjectDetailsById(projectModel.Id);
    
                if (_tempProject != null){
                    _tempProject.CreatorId = projectModel.UserId;
                    _tempProject.Description = projectModel.Description;
                    _context.Update < Project > (_tempProject);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Project Updated Successfully";
                }
            } catch (Exception ex) {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }
    }
}