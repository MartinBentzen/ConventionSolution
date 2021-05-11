using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repositories.DbEntities
{
    public class Convention
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string  Topic { get; set; }
        public int MaxCap { get; set; }
        public User Speaker { get; set; }
        public Convention() { }
    }
}
