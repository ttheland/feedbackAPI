using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using feedbackAPI.Entities;

namespace feedbackAPI.DTOs
{
    public record CreatePersonDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string JobTitle { get; set; }
        // [Range(1, 10)]
        public List<Project> Projects { get; set; }

    }
}