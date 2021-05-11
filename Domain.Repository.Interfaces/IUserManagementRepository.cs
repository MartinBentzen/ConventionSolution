using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Repository.Interfaces
{
    public interface IUserManagementRepository
    {
        Task CreateUser(User user);

        Task<User> FindUserByEmail(string email);
        Task<IEnumerable<Speaker>> GetSpeakers();
    }
}
