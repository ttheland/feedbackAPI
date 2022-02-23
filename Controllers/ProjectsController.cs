using System.Collections.Generic;
using feedbackAPI.Repositories;
using feedbackAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using feedbackAPI.DTOs;


namespace feedbackAPI.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepository repository;
        
        public ProjectsController(IProjectsRepository repository)
        {
            this.repository = repository;
        }

        // Routes
        // GET /projects
        [HttpGet]
        public IEnumerable<ProjectDTO> GetProjects()
        {
            var projects = repository.GetProjects().Select(project => project.AsDto());
            return projects;
        }

        // GET /projects/{id}
        [HttpGet("{id}")]
        public ActionResult<ProjectDTO> GetProject(Guid id)
        {
            var project = repository.getProject(id);

            if (project is null)
            {
                return NotFound();
            }

            return project.AsDto();
        }

        // POST /project
        [HttpPost]
        public ActionResult<ProjectDTO> CreateProject(CreateProjectDTO projectDTO)
        {
            Project project = new()
            {
                Id = Guid.NewGuid(),
                Name  = projectDTO.Name,
                Manager = projectDTO.Manager,
                Staff = projectDTO.Staff,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreateProject(project);

            return CreatedAtAction(nameof(GetProject), new {id = project.Id}, project.AsDto());

        }

        // PUT /projects/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProject(Guid id, UpdateProjectDTO projectDTO )
        {
            var existingProject = repository.getProject(id);

            if (existingProject is null)
            {
                return NotFound();
            }

            // taking copy of existingProject using with{} expression and providing updated info
            Project updateProject = existingProject with {
                Name = projectDTO.Name,
                Manager = projectDTO.Manager,
                Staff = projectDTO.Staff
            };

            repository.UpdateProject(updateProject);

            return NoContent();
        }

        // DEL /project/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProject(Guid id)
        {
            var existingProject = repository.getProject(id);

            if (existingProject is null)
            {
                return NotFound();
            }

            repository.DeleteProject(id);

            return NoContent();
        }
    }
}