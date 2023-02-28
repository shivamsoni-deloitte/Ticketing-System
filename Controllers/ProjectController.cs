using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tickeing_system.models;
using tickeing_system.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
namespace tickeing_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectController : ControllerBase
    {
        IProjectService _projectService;

        public ProjectController(IProjectService service){
            _projectService = service;
        }


        // Controller to get all Users
        [HttpGet]
        [Route("[action]"), Authorize]
        public IActionResult GetAllProjects() {
            try {
                var projects = _projectService.GetProjectList();
                if (projects == null) return NotFound();
                return Ok(projects);
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]/id"), Authorize]
        public IActionResult GetProjectById(int projectId) {
            try {
                var project = _projectService.GetProjectDetailsById(projectId);
                if (project == null) return NotFound();
                return Ok(project);
            } catch (Exception) {
                return BadRequest();
            }
        }


        // Controller to save Project
        [HttpPost]
        [Route("[action]"), Authorize]
        public IActionResult SaveProject(ProjectView projectModel) {
            try {
                var model = _projectService.SaveProject(projectModel);
                return Ok(model);
            } catch (Exception) {
                return BadRequest();
            }
        }

        // Controller to update Project
        [HttpPut]
        [Route("[action]"), Authorize]
        public IActionResult UpdateProject(ProjectView projectModel) {
            try {
                var model = _projectService.UpdateProject(projectModel);
                return Ok(model);
            } catch (Exception) {
                return BadRequest();
            }
        }

        // Controller to delete Project
        [HttpDelete]
        [Route("[action]"), Authorize]
        public IActionResult DeleteProject(int projectId) {
            try {
                var model = _projectService.DeleteProject(projectId);
                return Ok(model);
            } catch (Exception) {
                return BadRequest();
            }
        }
        

        // Controller to delete Project Issue
        [HttpDelete]
        [Route("[action]/projectId/issueId"), Authorize]
        public IActionResult  DeleteProjectIssue(int projectId, int issueId){
            try
            {
                 var model = _projectService.DeleteProjectIssue(projectId,issueId);
                return Ok(model);
            } catch (Exception) {
                return BadRequest();
            }
        }


    }
} 