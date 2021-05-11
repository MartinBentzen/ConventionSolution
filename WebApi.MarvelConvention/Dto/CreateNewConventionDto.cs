using System;

namespace WebApi.MarvelConvention.Dto
{
    public class CreateNewConventionDto
    {
        public string Name { get; set; }
        public string Topic  { get; set; }
        public int MaxCap { get; set; }
        public bool IsFull { get; set; }
        public string Description { get; set; }
        public Guid SpeakerId { get; set; }
    }
}