using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shooting_range_core.Models
{
    public partial class Tournament
    {
        public int Id { get; set; }
        [DisplayName("اسم الدورة")]
        [Required(ErrorMessage = "حقل 'اسم الدورة' مطلوب")]
        public string TournamentName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("تاريخ الدورة")]
        [Required(ErrorMessage = "حقل 'تاريخ الدورة' مطلوب")]
        public DateTime TournamentDate { get; set; }
        [Required(ErrorMessage = "حقل 'من' مطلوب")]
        [DisplayName("من")]
        public string FromTime { get; set; }

        [Required(ErrorMessage = "حقل 'الى' مطلوب")]
        [DisplayName("الى")]
        public string ToTime { get; set; }

        [DisplayName("نوع السلاح")]
        public string WeaponKind { get; set; }

        [DisplayName("الحقل")]
        public int? ShootingFieldId { get; set; }
        public ShootingField ShootingField { get; set; }
        public string ColorCode { get; set; }

        [DisplayName("ملاحظات")]
        public string Notes { get; set; }
        [NotMapped]
        public string ArabicDayName { get; set; }
    }
}
