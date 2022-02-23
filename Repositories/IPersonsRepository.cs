using System;
using System.Collections.Generic;
using feedbackAPI.Entities;

namespace feedbackAPI.Repositories
{
    // Interface for PersonsRepository
    public interface IPersonsRepository
    {
        Person getPerson(Guid id);
        IEnumerable<Person> GetPersons();

        void CreatePerson(Person person);

        void UpdatePerson(Person person);

        void DeletePerson(Guid id);

    }
}