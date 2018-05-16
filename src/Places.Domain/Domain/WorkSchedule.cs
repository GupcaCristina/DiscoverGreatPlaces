using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Places.Domain
{
   public class WorkSchedule:Entity<int>
    {
        public string StartWork { get; set; } 
        public string EndWork { get; set; }
        public bool HasLunchBreak { get; set; }
        public string StartBreak { get; set; }
        public string EndBreak { get; set; }
        public int PlaceId { get; set; }

        public DayOfWeek Day { get; set; }
    }
}
