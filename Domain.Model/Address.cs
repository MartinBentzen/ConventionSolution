using System;

namespace Domain.Model
{
    public class Address:BaseAddress
    {
        private Address(Guid id, string city, string road, string number, int postalCode)
        {
            Id = id;
            City = city;
            Road = road;
            Number = number;
            PostalCode = postalCode;
        }

        public static Address Create(string city, string road, string number, int postalCode)
        {
            if (city == null) throw new ArgumentNullException(nameof(city));
            if (road == null) throw new ArgumentNullException(nameof(road));
            if (number == null) throw new ArgumentNullException(nameof(number));
            if (postalCode <= 0) throw new ArgumentOutOfRangeException(nameof(postalCode));

            return new Address(Guid.NewGuid(), city, road, number, postalCode);
        }


        public override string GetCity()
        {
            return City;
        }

        public override string GetNumber()
        {
            return Number;
        }

        public override int GetPostalCode()
        {
            return PostalCode;
        }

        public override string GetRoad()
        {
            return Road;
        }
    }
}