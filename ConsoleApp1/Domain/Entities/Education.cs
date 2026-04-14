using ConsoleApp1.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Education : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public List<Group> Groups { get; set; }
    }
}
