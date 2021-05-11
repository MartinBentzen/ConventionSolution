using System;

namespace Domain.Model
{
    public class NotAvaliableAddress:BaseAddress
    {
        public override string GetCity() { throw new ArgumentNullException(nameof(City), "The City is not avaliable"); }
        public override string GetNumber() { throw new ArgumentNullException(nameof(City), "The Number is not avaliable"); }
        public override int GetPostalCode() { throw new ArgumentNullException(nameof(City), "The Number is not avaliable"); }
        public override string GetRoad() { throw new ArgumentNullException(nameof(City), "The Road is not avaliable"); }
        public static NotAvaliableAddress Create()
        {
            return new NotAvaliableAddress();
        }
    }
}