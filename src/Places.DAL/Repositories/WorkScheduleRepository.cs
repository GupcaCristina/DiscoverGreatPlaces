using Places.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Places.DAL.Repositories
{
    public class WorkScheduleRepository:IRepository<WorkSchedule>
    {
        public WorkScheduleRepository(DbContext context):base(context)
        {
            
        }

        public IQueryable<WorkSchedule> GetByPlace(int placeId)
        {
          return  Get().Where(p=>p.PlaceId==placeId)  ;
        }
    }
}
