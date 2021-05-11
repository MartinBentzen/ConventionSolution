using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Domain.Model
{
    public abstract class BaseSpeaker
    {
        protected string Name { get; set; }
        protected string Email { get; set; }
        protected Guid Id { get; set; }
        public abstract string GetName();
        public abstract string GetEmail();
        public abstract Guid GetId();
    }
    public class NotAvaliableSpeaker:BaseSpeaker
    {
        public override string GetName() { throw new ArgumentNullException(nameof(Name), "The Name is not avaliable"); }
        public override string GetEmail() { throw new ArgumentNullException(nameof(Name), "The Email is not avaliable"); }
        public override Guid GetId() { throw new ArgumentNullException(nameof(Name), "The Email is not avaliable"); }

        public static NotAvaliableSpeaker Create()
        {
            return new NotAvaliableSpeaker();
        }

       
    }
}
