using System;
using System.Collections.Generic;
using System.Linq;
using feedbackAPI.Entities;

namespace feedbackAPI.Repositories
{
    public class InMemFeedbackRepository : IPersonsRepository, IProjectsRepository
    {
        private readonly List<Person> persons = new()
        {
            new Person { Id = Guid.NewGuid(), Name = "Matti", JobTitle = "dev", Projects = null, CreatedDate = DateTimeOffset.UtcNow },
            new Person { Id = Guid.NewGuid(), Name = "Johnson", JobTitle = "Manager", Projects = null, CreatedDate = DateTimeOffset.UtcNow },
            new Person { Id = Guid.NewGuid(), Name = "Mark", JobTitle = "HR", Projects = null, CreatedDate = DateTimeOffset.UtcNow }
        };

        private readonly List<Project> projects = new()
        {
            new Project { Id = Guid.NewGuid(), Name = "Bou", Manager = null, CreatedDate = DateTimeOffset.UtcNow },
            new Project { Id = Guid.NewGuid(), Name = "Houston", Manager = null, CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Person> GetPersons()
        {
            return persons;
        }

        public Person getPerson(Guid id)
        {
            return persons.Where(person => person.Id == id).SingleOrDefault();
        }

        public void CreatePerson(Person person)
        {
            persons.Add(person);
        }

        public void UpdatePerson(Person person)
        {
            var index = persons.FindIndex(existingPerson => existingPerson.Id == person.Id);
            persons[index] = person;
        }

        public void DeletePerson(Guid id)
        {
            var index = persons.FindIndex(existingPerson => existingPerson.Id == id);
            persons.RemoveAt(index);
        }



        public IEnumerable<Project> GetProjects()
        {
            return projects;
        }

        public Project getProject(Guid id)
        {
            return projects.Where(project => project.Id == id).SingleOrDefault();
        }

        public void CreateProject(Project project)
        {
            projects.Add(project);
        }

        public void UpdateProject(Project project)
        {
            var index = projects.FindIndex(existingProject => existingProject.Id == project.Id);
            projects[index] = project;
        }

        public void DeleteProject(Guid id)
        {
            var index = projects.FindIndex(existingProject => existingProject.Id == id);
            projects.RemoveAt(index);
        }


    }
}