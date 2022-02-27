using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedback.Api.Controllers;
using Feedback.Api.DTOs;
using Feedback.Api.Entities;
using Feedback.Api.Repositories;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;


namespace Feedback.UnitTests
{
    // test class naming convention: UnitOfWork_StateUnderTest_ExpectedBehaviour
    public class PersonsControllerTests
    {
        private readonly Mock<IPersonsRepository> repositoryStub = new();
        private readonly Mock<ILogger<PersonsController>> loggerStub = new();
        private readonly Random rand = new();


        [Fact]
        public async Task GetPersonAsync_WithNullItem_ReturnsNotFound()
        {
            // arrange
            repositoryStub.Setup(repo => repo.getPersonAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Person)null);

            var controller = new PersonsController(repositoryStub.Object, loggerStub.Object);

            // act
            var result = await controller.GetPersonAsync(Guid.NewGuid());

            // assert
            Assert.IsType<NotFoundResult>(result.Result);

            // or with FluentAssertions:
            result.Result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task GetPersonAsync_WithExistingPerson_ReturnsExpectedPerson()
        {
            // arrange
            var expectedPerson = CreateRandomPerson(); 

            // return person with any guid
            repositoryStub.Setup(repo => repo.getPersonAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedPerson);
            
            var controller = new PersonsController(repositoryStub.Object, loggerStub.Object);

            // act
            var result = await controller.GetPersonAsync(Guid.NewGuid());
            
            // assert

            // Assert.IsType<PersonDTO>(result.Value);
            
            // var dto = (result as ActionResult<PersonDTO>).Value;
            // Assert.Equal(expectedPerson.Id, dto.Id);
            // Assert.Equal(expectedPerson.Name, dto.Name);

            // using FluentAssertions, instead of the previous we can do:
            result.Value.Should().BeEquivalentTo(
                expectedPerson,
                options => options.ComparingByMembers<Person>());
        }

        [Fact]
        public async Task GetPersonsAsync_WithExistingPersons_ReturnsAllPersons()
        {
            // arrange

            // create 3 random Persons
            var expectedPersons = new[] { CreateRandomPerson(), CreateRandomPerson(), CreateRandomPerson() };

            repositoryStub.Setup(repo => repo.GetPersonsAsync())
                .ReturnsAsync(expectedPersons);
            
            var controller = new PersonsController(repositoryStub.Object, loggerStub.Object);

            // act
            var actualPersons = await controller.GetPersonsAsync();

            // assert
            actualPersons.Should().BeEquivalentTo(
                expectedPersons,
                options => options.ComparingByMembers<Person>());
            
        }

        [Fact]
        public async Task CreatePersonAsync_WithPersonToCreate_ReturnsCreatedPerson()
        {
            // arrange
            var personToCreate = new CreatePersonDTO(){
                Name = Guid.NewGuid().ToString(),
                JobTitle = Guid.NewGuid().ToString(),
            };
            
            var controller = new PersonsController(repositoryStub.Object, loggerStub.Object);

            // act
            var result = await controller.CreatePersonAsync(personToCreate); 

            // assert
            // retrieve personDTO
            var createdPerson = (result.Result as CreatedAtActionResult).Value as PersonDTO;
            personToCreate.Should().BeEquivalentTo(
                createdPerson,
                options => options.ComparingByMembers<PersonDTO>().ExcludingMissingMembers()    // only compare properties existing in both
            );

            createdPerson.Id.Should().NotBeEmpty();
            createdPerson.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, 10.Seconds());
            
        }

        [Fact]
        public async Task UpdatePersonAsync_WithExistingPerson_ReturnsNoContent()
        {
            // arrange
             var existingPerson = CreateRandomPerson(); 

            // return person with any guid
            repositoryStub.Setup(repo => repo.getPersonAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingPerson);

            var personId = existingPerson.Id;
            var personToUpdate = new UpdatePersonDTO()
            {
                Name = Guid.NewGuid().ToString(),
                JobTitle = Guid.NewGuid().ToString()
            };
            
            var controller = new PersonsController(repositoryStub.Object, loggerStub.Object);

            // act

            var result = await controller.UpdatePersonAsync(personId, personToUpdate);

            // assert
            result.Should().BeOfType<NoContentResult>();
            
        }

        [Fact]
        public async Task DeletePersonAsync_WithExistingPerson_ReturnsNoContent()
        {
            // arrange
             var existingPerson = CreateRandomPerson(); 

            // return person with any guid
            repositoryStub.Setup(repo => repo.getPersonAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingPerson);

            
            var controller = new PersonsController(repositoryStub.Object, loggerStub.Object);

            // act
            var result = await controller.DeletePersonAsync(existingPerson.Id);


            // assert
            result.Should().BeOfType<NoContentResult>();
            
        }

        // method to create new Person with mock data
        private Person CreateRandomPerson()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                JobTitle = Guid.NewGuid().ToString(),
                Projects = null,
                CreatedDate = DateTimeOffset.UtcNow
            };
        }


    }
}
