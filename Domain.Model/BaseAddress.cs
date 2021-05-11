using System;

namespace Domain.Model
{
    public abstract class BaseAddress
    {
        public Guid Id { get; set; }

        public abstract string GetCity();
        public abstract string GetRoad();
        public abstract string GetNumber();
        public abstract int GetPostalCode();
        protected string City { get; set; }
        protected string Road { get; set; }
        protected string Number { get; set; }
        protected int PostalCode { get; set; }
    }
}