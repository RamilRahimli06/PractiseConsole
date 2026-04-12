using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    public class EducationWithGroupsResult
    {
        public Education Education { get; set; }
        public List<Group> Groups { get; set; }
    }
}
