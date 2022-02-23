using System;
using System.Collections.Generic;
using feedbackAPI.Entities;

namespace feedbackAPI.Repositories
{
    // Interface for ProjectsRepository
    public interface IProjectsRepository
    {
        Project getProject(Guid id);
        IEnumerable<Project> GetProjects();

        void CreateProject(Project project);

        void UpdateProject(Project project);

        void DeleteProject(Guid id);

    }
}