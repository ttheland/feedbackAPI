using System.Collections.Generic;
using Feedback.Api.Repositories;
using Feedback.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Feedback.Api.DTOs;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Feedback.Api.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsRepository repository;
        private readonly ILogger<PersonsController> logger;
        
        public PersonsController(IPersonsRepository repository, ILogger<PersonsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        // Routes
        // GET /persons
        [HttpGet]
        public async Task<IEnumerable<PersonDTO>> GetPersonsAsync()
        {
            var persons = (await repository.GetPersonsAsync())
                    .Select(person => person.AsDto());

            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {persons.Count()} records");
            
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