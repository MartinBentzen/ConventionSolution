using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Domain.Repository.Interfaces
{
    public interface IConventionsRepository
    {
        Task<IEnumerable<Convention>> GetConventions();
        Task CreateConvention(Convention convention);
        void AllocateConvention(ConventionParticipant conventionParticipant);

        Task<IEnumerable<Character>> GetMarvelCharacters();
    }
}