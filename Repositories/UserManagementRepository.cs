using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.DbEntities;
using Address = Domain.Model.Address;
using User = Domain.Model.User;

namespace Repositories
{
    public class UserManagementRepository:IUserManagementRepository
    {
        private readonly MarvelConventionDbContext _dbContext;

        public UserManagementRepository(MarvelConventionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateUser(User user)
        {
            var address = user.Address as Address;
            DbEntities.Address addressEntity = null; 

            if (address != null)
            {
                addressEntity = new DbEntities.Address {Id = address.Id, City = address.GetCity(), Road = address.GetRoad(), Number =  address.GetNumber(), PostalCode = address.GetPostalCode()};
            }
            
            var userEntity = new DbEntities.User {Id = user.Id, Name = user.Name, Email = user.Email, Password = user.Password, Address = addressEntity, Role = user.Role};
            await _dbContext.User.AddAsync(userEntity);

            _dbContext.SaveChanges();

        }

        public async Task<User> FindUserByEmail(string email)
        { 
            var user = await _dbContext.User.SingleOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return null;

            BaseAddress address = null;
            if (user.Address != null)
            {
                address = Address.Create(user.Address.City, user.Address.Road, user.Address.Number,
                    user.Address.PostalCode);
            }

            return User.Create(user.Id, user.Name, user.Email, user.Password, address, user.Role);
        }
        public async Task<IEnumerable<Speaker>> GetSpeakers()
        {
            var entitities = await _dbContext.User.Where(x => x.Role == "speaker").ToArrayAsync();
            var speakers = new List<Speaker>();
            foreach (var entity in entitities)
            {
                var speaker = Speaker.Create(entity.Id, entity.Name, entity.Email);
                speakers.Add(speaker);
            }

            return speakers;
        }
    }
}
