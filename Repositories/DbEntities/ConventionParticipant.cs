using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.DbEntities
{
    public class ConventionParticipant
    {
        public Guid Id { get; set; }
        public Guid ParticipantId { get; set; }
        public Guid ConventionId { get; set; }
        public bool IsReserved { get; set; }
    }
}
