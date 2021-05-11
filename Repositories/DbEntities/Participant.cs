using System;
using System.Collections.Generic;

namespace Repositories.DbEntities
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        public Participant() { }
    }
}