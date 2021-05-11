using System;

namespace WebApi.MarvelConvention.Dto
{
    public class AllocateConventionDto
    { 
        public Guid ConventionId { get; set; }
        public bool IsReserved { get; set; }
    }
}