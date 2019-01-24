using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Templates.WebAPI.Dtos.Users
{
    public class UserQueryDto
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public DateTime? StartCreationTime { get; set; }

        public DateTime? StopCreateionTime { get; set; }

    }
}
