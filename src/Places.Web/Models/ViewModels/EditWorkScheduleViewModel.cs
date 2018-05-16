using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Places.Web.Models.ViewModels
{
    public class EditWorkScheduleViewModel
    {
        public int Id { get; set; }
        public string StartWork { get; set; }
        public string EndWork { get; set; }
        public bool HasLunchBreak { get; set; }
        public string StartBreak { get; set; }
        public string EndBreak { get; set; }
        public int PlaceId { get; set; }

        public DayOfWeek Day { get; set; }
    }
}
