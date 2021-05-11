using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Schema;
using Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.DbEntities;
using Convention = Domain.Model.Convention;
using Participant = Domain.Model.Participant;
using ConventionParticipant = Domain.Model.ConventionParticipant;
using Domain.Model;


namespace Repositories
{
    public class ConventionsRepository:IConventionsRepository
    {
        private readonly MarvelConventionDbContext _dbContext;

        public ConventionsRepository(MarvelConventionDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Convention>> GetConventions()
        {

            var entities = await _dbContext.Conventions.Include(x => x.Speaker).ToArrayAsync();

            var list = new List<Convention>();
            foreach (var entity in entities)
            {
                Speaker speaker = null;
                if (entity.Speaker != null)
                {
                    speaker = Speaker.Create(entity.Speaker.Id, entity.Speaker.Name, entity.Speaker.Email);
                }
                list.Add(Convention.Create(entity.Id, entity.Name, entity.Topic, entity.MaxCap, 1, speaker));
            }
            //var query = await (from bb in _dbContext.Conventions
            //                   join cc in _dbContext.ConventionParticipants
            //                       on bb.Id equals cc.ConventionId where  cc.ParticipantId == 

            //var query = await (from bb in _dbContext.Conventions
            //    join cc in _dbContext.ConventionParticipants
            //        on bb.Id equals cc.ConventionId
            //    group new { bb, cc } by new { bb.Name, bb.MaxCap, bb.Id, bb.Topic, SpeakerId = bb.Speaker.Id } into newgroup
            //    select new
            //    {
            //        Id = newgroup.Key.Id,
            //        Speaker = newgroup.Key.SpeakerId,
            //        Name = newgroup.Key.Name,
            //        Topic = newgroup.Key.Topic,
            //        MaxCap = newgroup.Key.MaxCap,

                               //        Count = newgroup.Count()
                               //    }).ToListAsync();
                               //var conventions = new List<Convention>();
                               //foreach (var entity in query)
                               //{
                               //    Speaker speaker = null;
                               //    if (entity.SpeakerId != null)
                               //    {
                               //        speaker = Speaker.Create(entity.Speaker.Id, entity.Speaker.Name, entity.Speaker.Email);
                               //    }
                               //    var item = Convention.Create(entity.Id, entity.Name, entity.Topic, entity.MaxCap, entity.Count, speaker);
                               //    conventions.Add(item);
                               //}

            return list;
            
        }

        public async Task<IEnumerable<Character>> GetMarvelCharacters()
        {
            try
            {
                using var client = new HttpClient();
                string result = await client.GetStringAsync("http://gateway.marvel.com/v1/public/characters?ts=1&apikey=5abf43eeddc074475a9901d4b3402087&hash=975beff0256efec3b47392b0be337b2e");
                
                
                var root = JsonSerializer.Deserialize<Root>(result);
                return root.data.results.Select(x => new Character {Name = x.name, Description = x.description});
            }
            catch(Exception e)
            {
                throw new ArgumentException("An error occur, trying to fetch Marvel data", e);
            }
        }
        

        public async Task CreateConvention(Convention convention)
        {
            var speaker = convention.Speaker as Speaker;
            DbEntities.User user = null;

            if (speaker != null)
                user = _dbContext.User.Single(x => x.Id == speaker.GetId());
                
            await _dbContext.Conventions.AddAsync(new DbEntities.Convention {Id =  convention.Id, Name = convention.Name, Topic = convention.Topic, MaxCap = convention.MaxCap, Speaker = user});
            
            _dbContext.SaveChanges();
        }

        public void AllocateConvention(ConventionParticipant conventionParticipant)
        {
            var entity = new DbEntities.ConventionParticipant
            {
                Id = conventionParticipant.Id, 
                ConventionId = conventionParticipant.ConventionId,
                ParticipantId = conventionParticipant.ParticipantId,
                IsReserved = conventionParticipant.IsReserved
            };
            _dbContext.ConventionParticipants.Add(entity);

            _dbContext.SaveChanges();
        }
    }
   
}
