using System;
using System.Collections.Generic;

namespace feedbackAPI.Entities
{
    public record Person 
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string JobTitle { get; set; }

        public List<Project> Projects { get; set; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}