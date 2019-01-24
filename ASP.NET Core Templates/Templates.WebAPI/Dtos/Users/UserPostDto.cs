using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Templates.WebAPI.Dtos.Users
{
    /// <summary>
    /// 用户创建
    /// </summary>
    public class UserPostDto
    {
        [Required, MinLength(2), MaxLength(8)]
        public string UserName { get; set; }

        [Required, MinLength(2), MaxLength(4)]
        public string Name { get; set; }
    }
}
