using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Places.Web.Models.ViewModels
{
    public class WorkScheduleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Start work")]
        [StringLength(5, ErrorMessage = "The Start Work value cannot exceed 5 characters. ")]
        public string StartWork { get; set; }

        [Display(Name = "End work")]
        [StringLength(5, ErrorMessage = "The End work value cannot exceed 5 characters. ")]
        public string EndWork { get; set; }

        [Display(Name = "Has Lunch Break ")]
        public bool HasLunchBreak { get; set; }

        [Display(Name ="Start break")]
        [StringLength(5, ErrorMessage = "The Start Break  value cannot exceed 5 characters. ")]
        public string StartBreak { get; set; }

        [Display(Name = "End break")]
        [StringLength(5, ErrorMessage = "The End Break  value cannot exceed 5 characters. ")]
        public string EndBreak { get; set; }

        public int PlaceId { get; set; }

        public DayOfWeek Day { get; set; }
    }
}
