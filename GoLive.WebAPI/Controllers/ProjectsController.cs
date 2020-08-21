using System.Collections.Generic;
using AutoMapper;
using GoLive.Contracts;
using GoLive.Data;
using GoLive.Exceptions;
using GoLive.Helpers;
using GoLive.Models;
using GoLive.Models.ProjectDtos;
using Microsoft.AspNetCore.Mvc;

namespace GoLive.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, IMapper mapper)
        {
            _mapper = mapper;
            _projectService = projectService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateProject([FromBody]ProjectCreate model)
        {
            var project = _mapper.Map<Project>(model);
            var user = (User)HttpContext.Items["User"];
            project.ProjectCreatorId = user.UserId;
            _projectService.CreateProject(project);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            try
            {
                var entity = _projectService.GetProject(id);
                var project = _mapper.Map<ProjectDto>(entity);
                return Ok(project);
            }
            catch (ProjectNotFoundException)
            {
                var response = new { message = "Not Found" };
                return NotFound(response);
            }
        }

        [HttpPost("{projectId}/subscribe")]
        [Authorize]
        public IActionResult SubscribeToProject(int projectId)
        {
            var user = (User)HttpContext.Items["User"];
            var userId = user.UserId;
            if(_projectService.Subscribe(userId, projectId))
                return Ok();
            else return BadRequest();
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            var entities = _projectService.GetAllProjects();
            var projects = _mapper.Map<IEnumerable<ProjectListDto>>(entities);
            var response = new { data = projects };
            return Ok(projects);
        }
    }
}