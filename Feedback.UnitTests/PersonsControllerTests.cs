using System;
using System.Threading.Tasks;
using Feedback.Api.Controllers;
using Feedback.Api.DTOs;
using Feedback.Api.Entities;
using Feedback.Api.Repositories;
using FluentAssertions;
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
        public async Task GetPersonAsync_WithExistingId_ReturnsExpectedPerson()
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


        // method to create new person with mock data
        private Person CreateRandomPerson()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                JobTitle = Guid.NewGuid().ToString(),
                CreatedDate = DateTimeOffset.UtcNow
            };
        }


    }
}
