using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tickeing_system.models;

namespace tickeing_system.Services
{
    public interface IProjectService
    {
        List<Project> GetProjectList();
        ResponseModel SaveProject(ProjectView projectModel);

        Project GetProjectDetailsById(int projectId);

        ResponseModel DeleteProject(int projectId);

        ResponseModel UpdateProject(ProjectView projectModel);

        ResponseModel DeleteProjectIssue(int projectId, int issueId);
    }
}