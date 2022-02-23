using System.Collections.Generic;
using feedbackAPI.Repositories;
using feedbackAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using feedbackAPI.DTOs;


namespace feedbackAPI.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsRepository repository;
        
        public PersonsController(IPersonsRepository repository)
        {
            this.repository = repository;
        }

        // Routes
        // GET /persons
        [HttpGet]
        public IEnumerable<PersonDTO> GetPersons()
        {
            var persons = repository.GetPersons().Select(person => person.AsDto());
            return persons;
        }

        // GET /persons/{id}
        [HttpGet("{id}")]
        public ActionResult<PersonDTO> GetPerson(Guid id)
        {
            var person = repository.getPerson(id);

            if (person is null)
            {
                return NotFound();
            }

            return person.AsDto();
        }

        // POST /persons
        [HttpPost]
        public ActionResult<PersonDTO> CreatePerson(CreatePersonDTO personDTO)
        {
            Person person = new()
            {
                Id = Guid.NewGuid(),
                Name  = personDTO.Name,
                JobTitle = personDTO.JobTitle,
                Projects = personDTO.Projects,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreatePerson(person);

            return CreatedAtAction(nameof(GetPerson), new {id = person.Id}, person.AsDto());

        }

        // PUT /persons/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatePerson(Guid id, UpdatePersonDTO personDTO )
        {
            var existingPerson = repository.getPerson(id);

            if (existingPerson is null)
            {
                return NotFound();
            }

            // taking copy of existingPerson using with{} expression and providing updated info
            Person updatePerson = existingPerson with {
                Name = personDTO.Name,
                JobTitle = personDTO.JobTitle,
                Projects = personDTO.Projects
            };

            repository.UpdatePerson(updatePerson);

            return NoContent();
        }

        // DEL /persons/{id}
        [HttpDelete("{id}")]
        public ActionResult DeletePerson(Guid id)
        {
            var existingPerson = repository.getPerson(id);

            if (existingPerson is null)
            {
                return NotFound();
            }

            repository.DeletePerson(id);

            return NoContent();
        }
    }
}