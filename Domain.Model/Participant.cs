using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Domain.Model
{
    public class Participant:User1
    {
        //public Guid Id { get; private set; }
        //public string Name { get; private set; }
        public BaseAddress Address { get; private set; }
        public int PhoneNumber { get; private set; }
        public string Email { get; private set; }
        private Participant(Guid id, string name, BaseAddress address, int phoneNumber, string email):base(id, name)
        {
            //Id = id;
            //Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public static Participant Crate(string name, BaseAddress address, int phoneNumber, string email)
        {
            if (address == null)
            {
                address = NotAvaliableAddress.Create();
            }

            if (name == null) throw new ArgumentNullException(nameof(name));
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (phoneNumber <= 0) throw new ArgumentOutOfRangeException(nameof(phoneNumber));

            return new Participant(Guid.NewGuid(), name, address, phoneNumber, email);
        }
    }

    public abstract class User1
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
       
        public User1(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
