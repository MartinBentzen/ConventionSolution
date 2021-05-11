using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.MarvelConvention.Dto
{
    public class ConventionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public string IsFull { get; set; }
        public string HasSpeaker { get; set; }
    }
}
