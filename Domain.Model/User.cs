using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public BaseAddress Address { get; private set; }
        public string Role { get; private set; }

        private User(Guid id, string name, string email, string password, BaseAddress address, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Address = address;
            Role = role;
        }

        public static User Create(Guid id, string name, string email, string password, BaseAddress address, string role)
        {
            if (address == null)
            {
                address = NotAvaliableAddress.Create();
            }
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (password == null) throw new ArgumentNullException(nameof(password));

            return new User(id, name, email, password, address, role);
        }
    }
}
