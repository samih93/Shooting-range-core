using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shooting_range_core.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
