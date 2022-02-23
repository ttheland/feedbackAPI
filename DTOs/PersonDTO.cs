using System;
using System.Collections.Generic;
using feedbackAPI.Entities;

namespace feedbackAPI.DTOs
{
    
    public record PersonDTO 
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string JobTitle { get; set; }

        public List<Project> Projects { get; set; }

        public DateTimeOffset CreatedDate { get; init; }
    }

}