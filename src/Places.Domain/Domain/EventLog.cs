using System;
using System.Collections.Generic;
using System.Text;

namespace Places.Domain
{
    public  class EventLog:Entity<int>
    {
        public int? EventId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedTime { get; set; }
    }

    public enum LoggingEvents
    {            
        GET_ITEM=1000,
        LIST_ITEMS = 1001,
        INSERT_ITEM=1002,
        UPDATE_ITEM=1003,
        DELETE_ITEM=1004,      
    }
}
