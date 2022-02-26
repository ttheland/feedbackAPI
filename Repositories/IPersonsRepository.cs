using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using feedbackAPI.Entities;

namespace feedbackAPI.Repositories
{
    // Interface for PersonsRepository
    public interface IPersonsRepository
    {
        Task<Person> getPersonAsync(Guid id);
        Task<IEnumerable<Person>> GetPersonsAsync();

        Task CreatePersonAsync(Person person);

        Task UpdatePersonAsync(Person person);

        Task DeletePersonAsync(Guid id);

    }
}