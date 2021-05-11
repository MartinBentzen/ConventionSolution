using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class Speaker:BaseSpeaker
    {
        public override string GetName()
        {
            return Name;
        }

        public override string GetEmail()
        {
            return Email;
        }

        public override Guid GetId()
        {
            return Id;
        }

      
        private Speaker(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public static Speaker Create(Guid id, string name, string email)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (email == null) throw new ArgumentNullException(nameof(email));
            
            return new Speaker(id, name, email);
        }
    }
}
