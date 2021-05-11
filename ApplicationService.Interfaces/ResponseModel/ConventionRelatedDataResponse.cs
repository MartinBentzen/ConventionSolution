using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Model;

namespace ApplicationService.Interfaces.ResponseModel
{
    public class ConventionRelatedDataResponse
    {
        public IEnumerable<Character> Characters { get; private set; }
        public IEnumerable<SpeakerReponse> Speakers { get; private set; }

        private ConventionRelatedDataResponse(IEnumerable<Character> characters, IEnumerable<SpeakerReponse> speakers)
        {
            Characters = characters;
            Speakers = speakers;
        }

        public static ConventionRelatedDataResponse Create(IEnumerable<Character> characters, IEnumerable<Speaker> speakers)
        {
            if (characters == null) throw new ArgumentNullException(nameof(characters));
            if (speakers == null) throw new ArgumentNullException(nameof(speakers));

            return new ConventionRelatedDataResponse(characters, speakers.Select(x=> new SpeakerReponse{Id = x.GetId().ToString(), Name = x.GetName(), Email = x.GetEmail()}));
        }
    }
}
