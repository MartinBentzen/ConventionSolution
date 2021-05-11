using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class ConventionParticipant
    {
        public Guid Id { get; private set; }
        public Guid ParticipantId { get; private set; }
        public Guid ConventionId { get; private set; }
        public bool IsReserved { get; private set; }
        private ConventionParticipant(Guid id, Guid participantId, Guid conventionId, bool isReserved)
        {
            Id = id;
            ParticipantId = participantId;
            ConventionId = conventionId;
            IsReserved = isReserved;
        }

        public static ConventionParticipant Create(Guid id, Guid participantId, Guid conventionId, bool isReserved)
        {
            return new ConventionParticipant(id, participantId, conventionId, isReserved);
        }
    }
}
