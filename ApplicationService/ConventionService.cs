using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationService.Interfaces;
using ApplicationService.Interfaces.ResponseModel;
using Domain.Model;
using Domain.Repository.Interfaces;


namespace ApplicationService
{
    public class ConventionService:IConventionService
    {
        
        private readonly IConventionsRepository _conventionsRepository;
        private readonly IUserManagementRepository _userManagementRepository;
        
        public ConventionService(IConventionsRepository conventionsRepository, IUserManagementRepository userManagementRepository )
        {
            _conventionsRepository = conventionsRepository ?? throw new ArgumentNullException(nameof(conventionsRepository));
            _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
        }

        public async Task<IEnumerable<Convention>> GetConventions()
        {
            return await _conventionsRepository.GetConventions();
        }
        public async Task CreateConvention(Convention convention)
        {
            await _conventionsRepository.CreateConvention(convention);
        }

        public void AllocateConvention(ConventionParticipant conventionParticipant)
        {
            _conventionsRepository.AllocateConvention(conventionParticipant);
        }

        public async Task<ConventionRelatedDataResponse> GetConventionRelatedData()
        {
            IEnumerable<Character> characters = await _conventionsRepository.GetMarvelCharacters();

            IEnumerable<Speaker> speakers = await _userManagementRepository.GetSpeakers();

            return ConventionRelatedDataResponse.Create(characters, speakers);
        }
   }
}
