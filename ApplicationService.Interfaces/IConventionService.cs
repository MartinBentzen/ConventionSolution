using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationService.Interfaces.ResponseModel;
using Domain.Model;

namespace ApplicationService.Interfaces
{
    public interface IConventionService
    {
        Task CreateConvention(Convention convention);
        void AllocateConvention(ConventionParticipant conventionParticipant);
        Task<ConventionRelatedDataResponse> GetConventionRelatedData();
        Task<IEnumerable<Convention>> GetConventions();
    }
}
