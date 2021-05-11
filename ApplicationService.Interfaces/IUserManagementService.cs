using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Interfaces
{
    public interface IUserManagementService
    {
        Task Create(User user);
        Task<User> AuthenticateUser(string email, string password);
        Task<Speaker> GetSpeakerById(Guid id);
    }
}
