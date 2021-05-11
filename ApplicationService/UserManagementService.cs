using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.Interfaces;
using Domain.Model;
using Domain.Repository.Interfaces;

namespace ApplicationService
{
    public class UserManagementService:IUserManagementService
    {
        private readonly IUserManagementRepository _userManagementRepository;

        public UserManagementService(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        public async Task Create(User user)
        {
            await _userManagementRepository.CreateUser(user);
        }

        public async Task<Speaker> GetSpeakerById(Guid id)
        {
            var speakers = await _userManagementRepository.GetSpeakers();
            return speakers.SingleOrDefault(x => x.GetId() == id);
        }
        
        public async Task<User> AuthenticateUser(string email, string password)
        {
            var user = await _userManagementRepository.FindUserByEmail(email);

            if (user == null)
                return null;

            if (user.Password != password) return null;

            return user;
        }

    }
}
