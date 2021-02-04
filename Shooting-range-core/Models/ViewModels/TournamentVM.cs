using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shooting_range_core.Models.ViewModels
{
    public class TournamentVM
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public IEnumerable<SelectListItem> DDlYear { get; set; }
        public IEnumerable<SelectListItem> DDlMonth { get; set; }

        public string ArabicMonthName { get; set; }
        public int ShootingFieldId { get; set; }
        public IEnumerable<SelectListItem> DDLShootingField { get; set; }



        public ICollection<Tournament> Tournaments { get; set; }
    }
}
