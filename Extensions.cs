using feedbackAPI.DTOs;
using feedbackAPI.Entities;

namespace feedbackAPI
{
    public static class Extensions{
        public static PersonDTO AsDto(this Person person)
        {
            return new PersonDTO{
                Id = person.Id,
                Name = person.Name,
                JobTitle = person.JobTitle,
                Projects = person.Projects,
                CreatedDate = person.CreatedDate
            };
        }

        public static ProjectDTO AsDto(this Project project)
        {
            return new ProjectDTO{
                Id = project.Id,
                Name = project.Name,
                Manager = project.Manager,
                Staff = project.Staff,
                CreatedDate = project.CreatedDate
            };
        }
    }
}