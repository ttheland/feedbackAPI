using System;
using System.Collections.Generic;
using feedbackAPI.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace feedbackAPI.Repositories
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

        public void CreatePerson(Person person)
        {
            personsCollection.InsertOne(person);
        }

        public void CreateProject(Project project)
        {
            throw new NotImplementedException();
        }

        public void DeletePerson(Guid id)
        {
            var filter = personFilterBuilder.Eq(person => person.Id, id);
            personsCollection.DeleteOne(filter);
        }

        public void DeleteProject(Guid id)
        {
            throw new NotImplementedException();
        }

        public Person getPerson(Guid id)
        {
            var filter = personFilterBuilder.Eq(person => person.Id, id);
            return personsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Person> GetPersons()
        {
            return personsCollection.Find(new BsonDocument()).ToList();
        }

        public Project getProject(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetProjects()
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(Person person)
        {
            var filter = personFilterBuilder.Eq(existingPerson => existingPerson.Id, person.Id);
            personsCollection.ReplaceOne(filter, person);
        }

        public void UpdateProject(Project project)
        {
            throw new NotImplementedException();
        }
    }
}