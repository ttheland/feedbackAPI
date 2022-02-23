using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using feedbackAPI.Entities;

namespace feedbackAPI.DTOs
{
    public record CreateProjectDTO
    {
        [Required]
        public string Name { get; init; }
        public Person Manager { get; set; }

        public List<Person> Staff {get; set;}
    }
}