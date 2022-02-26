using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Feedback.Api.Entities;

namespace Feedback.Api.DTOs
{
    public record UpdateProjectDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public Person Manager { get; set; }
        // [Range(1, 10)]
        public List<Person> Staff { get; set; }

    }
}