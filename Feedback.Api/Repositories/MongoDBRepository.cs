using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Feedback.Api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Feedback.Api.Repositories
{
    public class MongoDBRepository : IPersonsRepository, IProjectsRepository
    {
        private const string databaseName = "feedbackAPIdb";



        private readonly IMongoCollection<Person> personsCollection;
        private readonly IMongoCollection<Project> projectsCollection;

        private readonly FilterDefinitionBuilder<Person> personFilterBuilder = Builders<Person>.Filter;
        private readonly FilterDefinitionBuilder<Project> projectFilterBuilder = Builders<Project>.Filter;

        public MongoDBRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            personsCollection = database.GetCollection<Person>("persons");
            projectsCollection = database.GetCollection<Project>("projects");

        }

        public async Task CreatePersonAsync(Person person)
        {
            await personsCollection.InsertOneAsync(person);
        }

        public void CreateProject(Project project)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePersonAsync(Guid id)
        {
            var filter = personFilterBuilder.Eq(person => person.Id, id);
            await personsCollection.DeleteOneAsync(filter);
        }

        public void DeleteProject(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Person> getPersonAsync(Guid id)
        {
            var filter = personFilterBuilder.Eq(person => person.Id, id);
            return await personsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            return await personsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public Project getProject(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetProjects()
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePersonAsync(Person person)
        {
            var filter = personFilterBuilder.Eq(existingPerson => existingPerson.Id, person.Id);
            await personsCollection.ReplaceOneAsync(filter, person);
        }

        public void UpdateProject(Project project)
        {
            throw new NotImplementedException();
        }
    }
}