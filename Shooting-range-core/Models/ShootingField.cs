using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shooting_range_core.Models
{
    public partial class ShootingField
    {
        public int Id { get; set; }
        [DisplayName("الحقل")]

        public string ShootingName { get; set; }
    }
}
