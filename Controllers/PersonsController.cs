using System.Collections.Generic;
using feedbackAPI.Repositories;
using feedbackAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using feedbackAPI.DTOs;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<PersonDTO>> GetPersonsAsync()
        {
            var persons = (await repository.GetPersonsAsync())
                    .Select(person => person.AsDto());
            return persons;
        }

        // GET /persons/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> GetPersonAsync(Guid id)
        {
            var person = await repository.getPersonAsync(id);

            if (person is null)
            {
                return NotFound();
            }

            return person.AsDto();
        }

        // POST /persons
        [HttpPost]
        public async Task<ActionResult<PersonDTO>> CreatePersonAsync(CreatePersonDTO personDTO)
        {
            Person person = new()
            {
                Id = Guid.NewGuid(),
                Name  = personDTO.Name,
                JobTitle = personDTO.JobTitle,
                Projects = personDTO.Projects,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreatePersonAsync(person);

            return CreatedAtAction(nameof(GetPersonAsync), new {id = person.Id}, person.AsDto());

        }

        // PUT /persons/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePersonAsync(Guid id, UpdatePersonDTO personDTO )
        {
            var existingPerson = await repository.getPersonAsync(id);

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

            await repository.UpdatePersonAsync(updatePerson);

            return NoContent();
        }

        // DEL /persons/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePersonAsync(Guid id)
        {
            var existingPerson = await repository.getPersonAsync(id);

            if (existingPerson is null)
            {
                return NotFound();
            }

            await repository.DeletePersonAsync(id);

            return NoContent();
        }
    }
}