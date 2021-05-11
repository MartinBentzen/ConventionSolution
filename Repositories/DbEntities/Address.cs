using System;

namespace Repositories.DbEntities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Road { get; set; }
        public string Number { get; set; }
        public int PostalCode { get; set; }

        public Address()
        {
            
        }

    }
}
