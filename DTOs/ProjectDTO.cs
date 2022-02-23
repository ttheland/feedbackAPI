using System;
using System.Collections.Generic;
using feedbackAPI.Entities;

namespace feedbackAPI.DTOs
{
    
   public record ProjectDTO
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public Person Manager {get; init; }

        public List<Person> Staff {get; set;}

        public DateTimeOffset CreatedDate { get; init; }
    }

}