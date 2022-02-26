using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Feedback.Api.Entities;

namespace Feedback.Api.DTOs
{
    public record UpdatePersonDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string JobTitle { get; set; }
        // [Range(1, 10)]
        public List<Project> Projects { get; set; }

    }
}