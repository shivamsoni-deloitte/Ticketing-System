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
    public class IssueController : ControllerBase
    {
        IIssueService _issueService;

        public IssueController(IIssueService service){
            _issueService = service;
        }

        [HttpGet]
        [Route("[action]/id"), Authorize]
        public IActionResult GetIssueById(int issueId) {
            try {
                var issue = _issueService.GetIssueDetailsById(issueId);
                if (issue == null) return NotFound();
                return Ok(issue);
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]/title"), Authorize]
        public IActionResult GetIssueByTitile(string issueTitle){
            try {
                var issue = _issueService.GetIssueByTitle(issueTitle);
                if (issue == null) return NotFound();
                return Ok(issue);
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]"), Authorize]
        public IActionResult GetAllIssuess() {
            try {
                var issues = _issueService.GetIssueList();
                if (issues == null) return NotFound();
                return Ok(issues);
            } catch (Exception) {
                return BadRequest();
            }
        }

        // Controller to create issue
        [HttpPost]
        [Route("[action]"), Authorize]
        public IActionResult CreateIssue(IssueView issueModel) {
            try {
                var model = _issueService.SaveIssue(issueModel);
                return Ok(model);
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("[action]"), Authorize]
        public IActionResult AddIssueLabel(LabelView labelModel){
            try
            {
                var model = _issueService.AddIssueLabel(labelModel);
                return Ok(model);
            }catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("[action]/issueId"), Authorize]
        public IActionResult UpdateIssueToNextStatus(int issueId){
            try
            {
                var model =_issueService.UpdateIssueToNextStatus(issueId);
                return Ok(model);
            }catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("[action]/issueId"), Authorize]
        public IActionResult ResetIssueStatus(int issueId){
            try
            {
                var model = _issueService.ResetIssueStatus(issueId);
                return Ok(model);
            }catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("[action]/issueId"), Authorize]
        public IActionResult UpdateProjectIssue(int issueId, IssueView issueModel){
            try
            {
                var model = _issueService.UpdateProjectIssue(issueId,issueModel);
                return Ok(model);
            }catch (Exception) {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("[action]"), Authorize]
        public IActionResult DeleteIssueLabel(int issueId, int labelId) {
            try {
                var model = _issueService.DeleteIssueLabel(issueId,labelId);
                return Ok(model);
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}