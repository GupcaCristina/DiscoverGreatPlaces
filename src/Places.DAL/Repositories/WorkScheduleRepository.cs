using Places.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Places.DAL.Repositories
{
    public class WorkScheduleRepository:Repository<WorkSchedule>
    {
        public WorkScheduleRepository(DbContext context):base(context)
        {
            
        }
    }
}
