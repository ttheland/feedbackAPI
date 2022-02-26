using System;
using System.Collections.Generic;
using feedbackAPI.Entities;
using MongoDB.Driver;

namespace feedbackAPI.Repositories
{
    public class MongoDBRepository : IPersonsRepository, IProjectsRepository
    {
        private const string databaseName = "feedbackAPIdb";



        private readonly IMongoCollection<Person> personsCollection;
        private readonly IMongoCollection<Project> projectsCollection;

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
            throw new NotImplementedException();
        }

        public void DeleteProject(Guid id)
        {
            throw new NotImplementedException();
        }

        public Person getPerson(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetPersons()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void UpdateProject(Project project)
        {
            throw new NotImplementedException();
        }
    }
}