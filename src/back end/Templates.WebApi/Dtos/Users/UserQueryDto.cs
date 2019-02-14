using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Templates.WebApi.Core.Dtos;

namespace Templates.WebApi.Dtos.Users
{
    public class UserQueryDto : QueryDto
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public DateTime? StartCreationTime { get; set; }

        public DateTime? StopCreateionTime { get; set; }

    }
}
